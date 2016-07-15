// XMLSettings.cpp : implementation file
//

#include "stdafx.h"

#include <msxml2.h>
#include "xmlfile.h"
#include <algorithm>

/********************************************************************/
/*																	*/
/* Function name : MakeSureDirectoryPathExists						*/
/* Description   : This function creates all the directories in		*/
/*				   the specified DirPath, beginning with the root.	*/
/*				   This is a clone a Microsoft function with the	*/
/*			       same name.										*/
/*																	*/
/********************************************************************/
BOOL MakeSureDirectoryPathExists(LPCTSTR lpszDirPath)
{
	CString strDirPath = lpszDirPath;
	strDirPath.Replace("/", "\\");

	if(strDirPath.CompareNoCase("New Item")==0)
		return FALSE;

	int nPos = 0;

	while((nPos = strDirPath.Find('\\', nPos+1)) != -1) 
	{
		CreateDirectory(strDirPath.Left(nPos), NULL);
	}
	return CreateDirectory(strDirPath, NULL);
}
CString BSTR2CString(BSTR pSrc)
{
	char * sValue =_com_util::ConvertBSTRToString(pSrc);
	if (pSrc) 
	{
		SysFreeString(pSrc); 
		pSrc = NULL; 
	}

	CString strValue = sValue;

	delete sValue;

	return strValue;
}
/////////////////////////////////////////////////////////////////////////////
// CXMLFile

CXMLFile::CXMLFile(const char* filename)
{
	::CoInitialize(NULL);
	if(XmlDocPtr!=NULL)
		XmlDocPtr.Detach();
	XmlDocPtr = NULL;

	char fullPath[_MAX_PATH];
	if (_fullpath(fullPath,filename,_MAX_PATH) == NULL)
		return;

	xml_file_name=fullPath;
}

CXMLFile::~CXMLFile()
{
	if(XmlDocPtr!=NULL)
	{
		XmlDocPtr.Release();
		XmlDocPtr.Detach();
		XmlDocPtr = NULL;
	}
}


void CXMLFile::clear(const char* root_name)
{
	if(XmlDocPtr!=NULL)
		XmlDocPtr.Detach();
	DeleteFile(xml_file_name.c_str());
	load(xml_file_name.c_str(), root_name);
}

// get a long value
long CXMLFile::GetLong(const char* cstrBaseKeyName, const char* cstrValueName, long lDefaultValue)
{
	/* 
		Since XML is text based and we have no schema, just convert to a string and 
		call the GetString method.
	*/
	long lRetVal = lDefaultValue;
	char chs[256];
	sprintf(chs,"%d", lRetVal);
	lRetVal = atol(GetString(cstrBaseKeyName, cstrValueName, chs).c_str() );
	return lRetVal;
}

std::string CXMLFile::GetNodeAttribute(MSXML2::IXMLDOMNodePtr &foundNode, const char* cstrAttributeName, const char* cstrDefaultAttributeValue)
{
	std::string strAttributeValue = cstrDefaultAttributeValue;

	try
	{
		if(cstrAttributeName!=NULL)
		{
			MSXML2::IXMLDOMElementPtr elptr=foundNode;
			strAttributeValue=BSTR2CString(
				_bstr_t( elptr->getAttribute(_bstr_t(cstrAttributeName)) )
				);
		}
	}
	catch(...)
	{
		return strAttributeValue;
	}

	return strAttributeValue;
}

std::string CXMLFile::GetAttribute(const char* cstrBaseKeyName, const char* cstrValueName, 
		const char* cstrAttributeName, const char* cstrDefaultAttributeValue)
{
	std::string strAttributeValue;
	std::string strDummy;
	GetNodeValue(cstrBaseKeyName, cstrValueName, "", strDummy, cstrAttributeName, 
		cstrDefaultAttributeValue, strAttributeValue);
	return strAttributeValue;

}

// get a string value
std::string CXMLFile::GetString(const char* cstrBaseKeyName, const char* cstrValueName, const char* cstrDefaultValue)
{
	std::string strValue;
	std::string strDummy;
	GetNodeValue(cstrBaseKeyName, cstrValueName, cstrDefaultValue, strValue, 
		NULL, NULL, strDummy);
	return strValue;
}

// set a long value
long CXMLFile::SetLong(const char* cstrBaseKeyName, const char* cstrValueName, long lValue)
{
	long lRetVal = 0;
	char chsVal[256];
	sprintf(chsVal,"%d", lValue);

	lRetVal = SetString(cstrBaseKeyName, cstrValueName, chsVal);

	return lRetVal;
}

// set a string value
long CXMLFile::SetString(const char* cstrBaseKeyName, const char* cstrValueName, const char* cstrValue)
{
	return SetNodeValue(cstrBaseKeyName, cstrValueName,cstrValue);
}

// set a string Attribute
long CXMLFile::SetAttribute(const char* cstrBaseKeyName, const char* cstrValueName,
					const char* cstrAttributeName, const char* cstrAttributeValue)
{
	return SetNodeValue(cstrBaseKeyName, cstrValueName, NULL, cstrAttributeName, cstrAttributeValue);
}

long CXMLFile::GetNodeValue(const char* cstrBaseKeyName, const char* cstrValueName, 
		const char* cstrDefaultValue, std::string& strValue, const char* cstrAttributeName, 
		const char* cstrDefaultAttributeValue,std::string& strAttributeValue)
{
	strValue = cstrDefaultValue;
	if(cstrDefaultAttributeValue!=NULL) strAttributeValue = cstrDefaultAttributeValue;

	int iNumKeys = 0;
	std::string cstrValue = cstrDefaultValue;
	std::string* pCStrKeys = NULL;

	std::string strBaseKeyName("//");
	strBaseKeyName +=cstrBaseKeyName;
	if( strBaseKeyName.at(strBaseKeyName.length() -1) !='/' )	
		strBaseKeyName += "/";
	strBaseKeyName += cstrValueName;

	MSXML2::IXMLDOMElementPtr rootElem = NULL;
	MSXML2::IXMLDOMNodePtr foundNode = NULL;
	foundNode=XmlDocPtr->selectSingleNode( _com_util::ConvertStringToBSTR(strBaseKeyName.c_str()) );
	if (foundNode)
	{
		try
		{
		// get the text of the node (will be the value we requested)
		BSTR bstr = NULL;
		HRESULT hr = foundNode->get_text(&bstr);
		strValue =BSTR2CString(bstr);
		
		if(cstrAttributeName!=NULL)
		{
			MSXML2::IXMLDOMElementPtr elptr=foundNode;
			strAttributeValue=BSTR2CString(
				_bstr_t( elptr->getAttribute(_bstr_t(cstrAttributeName)) )
				);
		}
		}
		catch(...)
		{
			return S_FALSE;
		}

		return 0;
	}
	else
		return -1;

}

long CXMLFile::SetNodeValue(const char* cstrBaseKeyName, const char* cstrValueName, 
			const char* cstrValue, const char* cstrAttributeName, const char* cstrAttributeValue)
{
	/*  RETURN VALUES:
		 0 = SUCCESS		-1 = LOAD FAILED		-2 = NODE NOT FOUND
		-3 = PUT TEXT FAILED		-4 = SAVE FAILED
	*/
	long lRetVal = 0;
	int iNumKeys = 0;
	std::string* pCStrKeys = NULL;

	// Add the value to the base key separated by a '\'
	std::string strBaseKeyName(cstrBaseKeyName);
	if( strBaseKeyName.at(strBaseKeyName.length() -1) !='/' )	
		strBaseKeyName += "/";
	strBaseKeyName += cstrValueName;

	// Parse all keys from the base key name (keys separated by a '\')
	pCStrKeys = ParseKeys(strBaseKeyName.c_str(), iNumKeys);

	// Traverse the xml using the keys parsed from the base key name to find the correct node
	if (pCStrKeys)
	{	
		if (XmlDocPtr == NULL)
			return -2;

		MSXML2::IXMLDOMElementPtr rootElem = NULL;
		MSXML2::IXMLDOMNodePtr foundNode = NULL;
		
		XmlDocPtr->get_documentElement(&rootElem);  // root node
		
		if (rootElem)
		{
			// returns the last node in the chain
			foundNode = FindNode(rootElem, pCStrKeys, iNumKeys, true); 
			
			if (foundNode)
			{
				HRESULT hr;
				// set the text of the node (will be the value we sent)
				if(cstrValue!=NULL)
					hr = foundNode->put_text(_bstr_t(cstrValue));
				if(cstrAttributeName!=NULL )
				{
					MSXML2::IXMLDOMElementPtr elptr=foundNode;
					hr = elptr->setAttribute(_bstr_t(cstrAttributeName),
						_bstr_t(cstrAttributeValue) );
				}		

				if (!SUCCEEDED(hr))				
					lRetVal = -3;
				foundNode = NULL;
			}
			else
				lRetVal = -2;
			
			rootElem = NULL;
		}

		delete [] pCStrKeys;
	}

	return lRetVal;
}

// xmlfile.DeleteSetting("Settings/who","");删除该键及其所有子键
// delete a key or chain of keys
long CXMLFile::DeleteSetting(const char* cstrBaseKeyName, const char* cstrValueName)
{
	long bRetVal = -1;
	int iNumKeys = 0;
	std::string* pCStrKeys = NULL;
	std::string strBaseKeyName(cstrBaseKeyName);
	
	// Parse all keys from the base key name (keys separated by a '\')
	pCStrKeys = ParseKeys(strBaseKeyName.c_str(), iNumKeys);

	// Traverse the xml using the keys parsed from the base key name to find the correct node.
	if (pCStrKeys)
	{
		if (XmlDocPtr == NULL)
			return bRetVal;
		MSXML2::IXMLDOMElementPtr rootElem = NULL;
		MSXML2::IXMLDOMNodePtr foundNode = NULL;
		XmlDocPtr->get_documentElement(&rootElem);  // root node
		while (rootElem)
		{
			// returns the last node in the chain
			foundNode = FindNode(rootElem, pCStrKeys, iNumKeys, FALSE, cstrValueName); 
			if (foundNode)
			{
				// get the parent of the found node and use removeChild to delete the found node
				MSXML2::IXMLDOMNodePtr parentNode = NULL;
				
				foundNode->get_parentNode(&parentNode);

				CComBSTR bstr = NULL;
				HRESULT hr = foundNode->get_text(&bstr);
				CString strValue = CString(bstr.m_str);
//				CString strValue =BSTR2CString(bstr);
				
				if (parentNode&&(strValue == cstrValueName || cstrValueName == ""))
				{
					if(cstrValueName != "")
						foundNode = parentNode;

					foundNode->get_parentNode(&parentNode);
					HRESULT hr = parentNode->removeChild(foundNode);
					parentNode = NULL;
				}
				foundNode = NULL;
			}
			else
				rootElem = NULL;
		}
		delete [] pCStrKeys;
	}
	return bRetVal;
}

// get a basekey's all children's value
long CXMLFile::GetKeysValue(const char* cstrBaseKeyName, std::map<std::string, std::string>& keys_val)
{
	int iNumKeys = 0;
	std::string* pCStrKeys = NULL;
	std::string strValue;

	pCStrKeys = ParseKeys(cstrBaseKeyName, iNumKeys);

	if (pCStrKeys)
	{
		if (XmlDocPtr == NULL)  // load the xml document
			return -1;
		MSXML2::IXMLDOMElementPtr rootElem = NULL;
		MSXML2::IXMLDOMNodePtr foundNode = NULL;
		MSXML2::IXMLDOMNodeListPtr nodelst= NULL;
		MSXML2::IXMLDOMNodePtr pNode= NULL;
		XmlDocPtr->get_documentElement(&rootElem);  // root node

		if (rootElem)
		{
			foundNode = FindNode(rootElem, pCStrKeys, iNumKeys); 
			if (foundNode)
				nodelst=foundNode->GetchildNodes();
			if(nodelst!=NULL)
			{
				for (int i=0; i<nodelst->length; i++)
				{
					pNode = nodelst->item[i];
					keys_val[(const char*)(pNode->nodeName)]=pNode->text;//(const char*)pNode->xml;					
				}
				foundNode = NULL;
			}
			pNode=NULL;
			nodelst= NULL;
			foundNode = NULL;
			rootElem = NULL;
		}
		delete [] pCStrKeys;
	}
	return 0;
}

// get a basekey's all children's value
long CXMLFile::GetKeys(const char* cstrBaseKeyName, std::vector<std::string>& keys)
{
	int iNumKeys = 0;
	std::string* pCStrKeys = NULL;
	std::string strValue;

	pCStrKeys = ParseKeys(cstrBaseKeyName, iNumKeys);

	if (pCStrKeys)
	{
		if (XmlDocPtr == NULL)  // load the xml document
			return -1;
		MSXML2::IXMLDOMElementPtr rootElem = NULL;
		MSXML2::IXMLDOMNodePtr foundNode = NULL;
		MSXML2::IXMLDOMNodePtr pNode= NULL;
		XmlDocPtr->get_documentElement(&rootElem);  // root node

		if (rootElem)
		{
			foundNode = FindNode(rootElem, pCStrKeys, iNumKeys);
			pNode=foundNode->GetfirstChild();
			while(pNode!=NULL)
			{
				//pNode =pNode-> nodelst->item[i];
				keys.push_back ((const char*)pNode->nodeName);//(const char*)pNode->xml;	
				//ATLTRACE((const char*)pNode->text);ATLTRACE("\n==============");
				pNode=pNode->GetnextSibling();
			}
		}
		delete [] pCStrKeys;
	}
	return 0;
}

long CXMLFile::GetRootElem(MSXML2::IXMLDOMElementPtr rootElem)
{
	return XmlDocPtr->get_documentElement(&rootElem);
}

long CXMLFile::GetNode(const char* cstrKeyName,
					   MSXML2::IXMLDOMNodePtr& foundNode)
{
	int iNumKeys = 0;
	std::string* pCStrKeys = NULL;

	std::string strBaseKeyName( "//");
	strBaseKeyName +=cstrKeyName;

	foundNode=XmlDocPtr->selectSingleNode( _com_util::ConvertStringToBSTR(strBaseKeyName.c_str()) );
	if (foundNode)
	{
		return 0;
	}
	else
		return -1;

}

long CXMLFile::GetNodeList(const char* cstrKeyName,
					   MSXML2::IXMLDOMNodeListPtr& foundNodeList)
{
	int iNumKeys = 0;
	std::string* pCStrKeys = NULL;

	std::string strBaseKeyName( "//");
	strBaseKeyName +=cstrKeyName;

	foundNodeList=XmlDocPtr->selectNodes( _com_util::ConvertStringToBSTR(strBaseKeyName.c_str()) );
	if (foundNodeList)
	{
		return 0;
	}
	else
		return -1;

}

// Parse all keys from the base key name.
std::string* CXMLFile::ParseKeys(const char* cstrFullKeyPath, int &iNumKeys)
{
	std::string cstrTemp;
	std::string* pCStrKeys = NULL;
	std::string strFullKeyPath(cstrFullKeyPath);
	// replace spaces with _ since xml doesn't like them
	std::replace(strFullKeyPath.begin(), strFullKeyPath.end(), ' ', '_');
	
	if (*(strFullKeyPath.end() - 1) == '/' )
		strFullKeyPath.erase(strFullKeyPath.end() -1 );// remove slashes on the end

	iNumKeys=std::count(strFullKeyPath.begin(), strFullKeyPath.end(), '/') +1;

	pCStrKeys = new std::string[iNumKeys];  // create storage for the keys

	if (pCStrKeys)
	{
		int iFind = 0, iLastFind = 0, iCount = -1;
		
		// get all of the keys in the chain
		while (iFind != -1)
		{
			iFind = strFullKeyPath.find('/', iLastFind);
			if (iFind > -1)
			{
				iCount++;
				pCStrKeys[iCount].assign(strFullKeyPath, iLastFind, iFind - iLastFind);
				iLastFind = iFind + 1;
			}
			else
			{
				// make sure we don't just discard the last key in the chain
				if (iLastFind < strFullKeyPath.length()) 
				{
					iCount++;
					pCStrKeys[iCount].assign(strFullKeyPath, iLastFind, strFullKeyPath.length() - iLastFind);
				}
			}
		}
	}

	return pCStrKeys;
}

//if the specific file exist, then return true, else return false
static BOOL FileExist(const char* pszFileName)
{
	BOOL bExist = false;
	HANDLE hFile;
	
	if (NULL != pszFileName)
	{
		// Use the preferred Win32 API call and not the older OpenFile.
		hFile = CreateFile(pszFileName, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE,
			NULL, OPEN_EXISTING, 0, 0);
		
		if (hFile != INVALID_HANDLE_VALUE)
		{
			// If the handle is valid then the file exists.
			CloseHandle(hFile);
			bExist = true;
		}
	}
	
	return (bExist);
}

// load the XML file into the parser
int CXMLFile::load(const char* filename, const char* root_name)
{
	int nRet = false;

	if(XmlDocPtr != NULL)
		return nRet;
	
	VARIANT_BOOL vbSuccessful;
	xml_file_name=filename;
	// initialize the Xml parser
	HRESULT hr = XmlDocPtr.CreateInstance(__uuidof(MSXML2::DOMDocument30));
		//__uuidof(MSXML2::DOMDocument40));
	//(MSXML2::CLSID_DOMDocument);
		
	if (XmlDocPtr == NULL) return nRet;

	m_root_name=root_name;
	// see if the file exists
	if ( !FileExist(filename ) )  // if not
	{
		// create it
		std::string strtmp("<?xml version=\"1.0\" ?><");//encoding=\"UTF-16\"
		if(root_name!=NULL)
		{
			strtmp+=root_name;
			strtmp+="></";
			strtmp+=root_name;
			strtmp+=">";
		}
		else
			strtmp+="xmlRoot></xmlRoot>";

		vbSuccessful=XmlDocPtr->loadXML(_bstr_t(strtmp.c_str()));

		nRet = 2;
	}
	else  // if so
	{
		// load it
		vbSuccessful=XmlDocPtr->load(CComVariant::CComVariant((const char*)filename ));
		nRet = true;
	}

	if (vbSuccessful == VARIANT_TRUE)
	{
		return nRet;  // loaded		
	}
	else
	{
		// create it
		std::string strtmp("<?xml version=\"1.0\" ?><");//encoding=\"UTF-16\"
		if(root_name!=NULL)
		{
			strtmp+=root_name;
			strtmp+="></";
			strtmp+=root_name;
			strtmp+=">";
		}
		else
			strtmp+="xmlRoot></xmlRoot>";

		vbSuccessful=XmlDocPtr->loadXML(_bstr_t(strtmp.c_str()));

		nRet = 2;
		return nRet;
	}	
}

// save the XML file
BOOL CXMLFile::save(const char* filename)
{ 
	if(XmlDocPtr==NULL)
		return false;
	HRESULT hr;
	if(filename==NULL||filename=="")
	{
		CString strFilePath = xml_file_name.c_str();
		strFilePath.Replace("/", "\\");
		int nPos = strFilePath.ReverseFind('\\');
		strFilePath = strFilePath.Left(nPos);
		MakeSureDirectoryPathExists(strFilePath);
		hr = XmlDocPtr->save(CComVariant::CComVariant(xml_file_name.c_str()));
	}
	else
	{
		hr = XmlDocPtr->save(CComVariant::CComVariant(filename));
		xml_file_name=filename;
	}
	//XmlDocPtr=NULL;
	return SUCCEEDED(hr);
}

// discard any changes
void CXMLFile::DiscardChanges()
{
	XmlDocPtr=NULL;	
}

// find a node given a chain of key names
MSXML2::IXMLDOMNodePtr CXMLFile::FindNode(MSXML2::IXMLDOMNodePtr parentNode, 
											  std::string* pCStrKeys, int iNumKeys, 
												  BOOL bAddNodes /*= false*/, CString strKeyValue)
{
	MSXML2::IXMLDOMNodePtr foundNode = NULL;
	MSXML2::IXMLDOMElementPtr tempElem = NULL;

	CString strParentNodeName;
	{
		// 获取节点名
		BSTR bstr = NULL;
		HRESULT hr = parentNode->get_nodeName(&bstr);
		strParentNodeName =BSTR2CString(bstr);
	}

	for (int i=0; i<iNumKeys; i++)
	{
		// find the node named X directly under the parent
		if(!strcmp(pCStrKeys[i].c_str(), strParentNodeName))
			foundNode = parentNode;
		else
			foundNode = parentNode->selectSingleNode(_bstr_t(pCStrKeys[i].c_str()));

		if (foundNode == NULL) 
		{
			// if its not found...
			if (bAddNodes)  // create the node and append to parent (Set only)
			{
				tempElem=XmlDocPtr->createElement(_bstr_t(pCStrKeys[i].c_str()));
				if (tempElem) 
				{
					foundNode=parentNode->appendChild(tempElem);
					// since we are traversing the nodes, we need to set the parentNode to our foundNode
					parentNode = NULL;
					parentNode = foundNode;
					foundNode = NULL;
				}
			}
			else
			{
				foundNode = NULL;
				parentNode = NULL;
				break;
			}
		}
		else
		{
			parentNode = NULL;
			parentNode = foundNode;
			foundNode = NULL;
		}
	}

	if(strKeyValue != "" && parentNode)
	{
		parentNode = parentNode->GetparentNode();

		while(parentNode != NULL)
		{
			foundNode = parentNode->selectSingleNode(_bstr_t(pCStrKeys[iNumKeys-1].c_str()));

			BSTR bstr = NULL;
			HRESULT hr = foundNode->get_text(&bstr);
			CString strValue =BSTR2CString(bstr);

			if(strValue != strKeyValue)
			{
				parentNode = parentNode->GetnextSibling();
			}
			else
			{
				parentNode = foundNode;
				break;
			}
		}
	}

	return parentNode;
}

// add a node given a chain of key names
MSXML2::IXMLDOMNodePtr CXMLFile::AddNode(MSXML2::IXMLDOMNodePtr parentNode, 
										  std::string* pCStrKeys, int iNumKeys)
{
	MSXML2::IXMLDOMNodePtr foundNode = NULL;
	MSXML2::IXMLDOMElementPtr tempElem = NULL;

	for (int i=0; i<iNumKeys; i++)
	{
		tempElem=XmlDocPtr->createElement(_bstr_t(pCStrKeys[i].c_str()));
		if (tempElem) 
		{
			foundNode=parentNode->appendChild(tempElem);
			// since we are traversing the nodes, we need to set the parentNode to our foundNode
			parentNode = NULL;
			parentNode = foundNode;
			foundNode = NULL;
		}
	}

	return parentNode;
}

//////////////////////////////////////////////////////////////////////////
/* 用法示例
//test xmlFile

CXMLFile xfile("C:/projects/SimOnQt/src/projects/regress/regress.xml");
if(!xfile.Open()) return;

//  xfile.RemoveAllValue("Schedule");
MSXML2::IXMLDOMElementPtr nodeElem = xfile.AddNodeElem("/", "Schedule");
xfile.AddChildElem(nodeElem, "Timeline", "hello");
xfile.AddChildElem(nodeElem, "Delivery", "hello");
xfile.AddChildElem(nodeElem, "Note", "hello");
xfile.Save();

//end test xmlFile
*/
BOOL CXMLFile::IsOpen()
{
	return (XmlDocPtr != NULL);
}

int CXMLFile::Open()
{
	return load(xml_file_name.c_str(), "XmlFile");
}

void CXMLFile::Save()
{
	save();
}

CString CXMLFile::firstNodeTag(CString &strNodeTag)
{
	CString strFirstNodeTag = "";
	int nPos = strNodeTag.Find("/");
	if(nPos>=0)
	{
		strFirstNodeTag = strNodeTag.Left(nPos);
		strNodeTag = strNodeTag.Right(strNodeTag.GetLength() - nPos - 1);
	}
	else
	{
		strFirstNodeTag = strNodeTag;
	}

	return strFirstNodeTag;
}

// 这里strNodeTag需要给节点的完整路径（不包括根“XmlFile”） 例子：“Schedule/Timeline”
// 删除“XmlFile/Schedule”下的所有记录
void CXMLFile::RemoveAllValue(CString strNodeTag)
{
	DeleteSetting(strNodeTag.GetBuffer(strNodeTag.GetLength()), "");
}

void CXMLFile::RemoveValue(CString strNodeTag, CString strKey)
{
	DeleteSetting(strNodeTag.GetBuffer(strNodeTag.GetLength()), strKey.GetBuffer(strKey.GetLength()));
}

// 这里strNodeTag需要给节点的完整路径（不包括根“XmlFile”） 例子：“Schedule/Timeline”
// “/”表示根节点
MSXML2::IXMLDOMElementPtr CXMLFile::GetDomElement(CString strNodeTag)
{
	int iNumKeys = 0;
	std::string* pCStrKeys = NULL;
	// Parse all keys from the base key name (keys separated by a '\')


	MSXML2::IXMLDOMElementPtr rootElem = NULL;
	MSXML2::IXMLDOMNodePtr foundNode = NULL;
	XmlDocPtr->get_documentElement(&rootElem);  // root node

	if(strNodeTag != "/")
	{
		pCStrKeys = ParseKeys(strNodeTag, iNumKeys);
		foundNode = FindNode(rootElem, pCStrKeys, iNumKeys, true); 
		delete[] pCStrKeys;
	}
	else
	{
		foundNode = rootElem;
	}

	return foundNode;
}

// 这里strNodeTag需要给节点的完整路径（不包括根“XmlFile”） 例子：“Schedule/Timeline”
// “/”表示根节点
CString CXMLFile::GetElemValue(CString strNodeTag)
{
	return GetString("", strNodeTag, "").c_str();
}

MSXML2::IXMLDOMElementPtr CXMLFile::AddNodeElem(CString strParentNodeTag, CString strChildTag, CString strChildValue)
{
	int iNumKeys = 0;
	std::string* pCStrKeys = NULL;
	// Parse all keys from the base key name (keys separated by a '\')


	MSXML2::IXMLDOMElementPtr rootElem = NULL;
	MSXML2::IXMLDOMNodePtr foundNode = NULL;
	XmlDocPtr->get_documentElement(&rootElem);  // root node

	if(strParentNodeTag != "/")
	{
		pCStrKeys = ParseKeys(strParentNodeTag, iNumKeys);
		foundNode = FindNode(rootElem, pCStrKeys, iNumKeys, true); 
		delete[] pCStrKeys;
	}
	else
	{
		foundNode = rootElem;
	}

	pCStrKeys = ParseKeys(strChildTag, iNumKeys);
	MSXML2::IXMLDOMNodePtr addNode = AddNode(foundNode, pCStrKeys, iNumKeys); 
	delete[] pCStrKeys;

	addNode->put_text(_bstr_t(strChildValue));

	return addNode;
}

MSXML2::IXMLDOMElementPtr CXMLFile::AddChildElem(MSXML2::IXMLDOMElementPtr ParentElem, CString strChildTag, CString strChildValue)
{
	if(strChildValue == "") strChildValue = "　";

	int iNumKeys = 0;
	std::string* pCStrKeys = NULL;
	// Parse all keys from the base key name (keys separated by a '\')
	pCStrKeys = ParseKeys(strChildTag, iNumKeys);

	MSXML2::IXMLDOMNodePtr foundNode = FindNode(ParentElem, pCStrKeys, iNumKeys, true); 
	delete[] pCStrKeys;

	foundNode->put_text(_bstr_t(strChildValue));

	return foundNode;
}

//////////////////////////////////////////////////////////////////////////

void CXMLFile::ChangeXSLPath(CString strXSLPath)
{
	MSXML2::IXMLDOMNodePtr foundNode = NULL;

	foundNode = XmlDocPtr->selectSingleNode("pi(\"xml-stylesheet\")");

	CString strValue;
	strValue.Format("type=\"text/xsl\" href=\"%s\"", strXSLPath);

	if(foundNode) foundNode->text = strValue.GetBuffer(strValue.GetLength());
}