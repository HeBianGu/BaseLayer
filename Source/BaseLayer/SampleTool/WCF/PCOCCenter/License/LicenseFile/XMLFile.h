#ifndef _XMLFILE_H_
#define _XMLFILE_H_

#pragma warning(disable: 4786)

#import <msxml3.dll>

#include <atlbase.h>
#include <string>
#include <map>
#include <vector>

//typedef const char* LPCTSTR;

//!  XMLFile class ��дXML�ļ� 
/*!
  ����������Ӧ�ó������á�
  CXMLFile *xml = new CXMLFile("E:\\6.0\\temp\\applicense.xml");
  xml->Open();
  string sss = xml->GetString("LicenseInfo", "Customer", "");
  string sss1 = xml->GetAttribute("LicenseInfo/Applications", "Application", "Name", "");
  delete xml;
*/

class CXMLFile
{
/// Construction
public:
	CXMLFile(const char* filename);
	
	virtual ~CXMLFile();


public:
	CString GetFileName() { return xml_file_name.c_str(); }
	BOOL IsOpen();
	int Open();
	void Save();

	void RemoveAllValue(CString strNodeTag);
	void RemoveValue(CString strNodeTag, CString strKey);
	MSXML2::IXMLDOMElementPtr GetDomElement(CString strNodeTag);
	CString GetElemValue(CString strNodeTag);
	MSXML2::IXMLDOMElementPtr AddNodeElem(CString strParentNodeTag, CString strChildTag, CString strChildValue="");
	MSXML2::IXMLDOMElementPtr AddChildElem(MSXML2::IXMLDOMElementPtr ParentElem, CString strChildTag, CString strChildValue="");

	void ChangeXSLPath(CString strXSLPath);

private:
	CString firstNodeTag(CString &strNodeTag);


/// Implementation
public:

	///�õ��ڵ�ֵ(������)
	///����XML�ļ�
	/** 
		\param cstrBaseKeyName ������.
		\param cstrValueName ����(ȡֵ��).
		\param lDefaultValue Ĭ�ϳ���ֵ.
	*/
	long GetLong(const char* cstrBaseKeyName, const char* cstrValueName, long lDefaultValue);
	///���ýڵ�ֵ(������)
	/** 
		\param cstrBaseKeyName ������.
		\param cstrValueName ����(ȡ����ֵ��).
		\param lDefaultValue Ĭ�ϳ���ֵ.
	*/
	long SetLong(const char* cstrBaseKeyName, const char* cstrValueName, long lValue);

	///�õ��ڵ�ֵ(�ַ���)
	/** 
		\param cstrBaseKeyName ������.
		\param cstrValueName ����(ȡֵ��).
		\param cstrDefaultValue Ĭ���ַ���ֵ.
	*/
	std::string GetString(const char* cstrBaseKeyName, const char* cstrValueName, const char* cstrDefaultValue);
	///���ýڵ�ֵ(�ַ���)
	/** 
		\param cstrBaseKeyName ������.
		\param cstrValueName ����(����ֵ��).
		\param cstrDefaultValue Ĭ���ַ���ֵ.
	*/
	long SetString(const char* cstrBaseKeyName, const char* cstrValueName, const char* cstrValue);
	
	///�õ��ڵ�����
	/** 
		\param cstrBaseKeyName ������.
		\param cstrValueName ����(�������Լ���).
		\param cstrAttributeName ������(��������ֵ��).
		\param cstrDefaultAttributeValue Ĭ������ֵ.
	*/
	std::string GetAttribute(const char* cstrBaseKeyName, const char* cstrValueName, 
		const char* cstrAttributeName, const char* cstrDefaultAttributeValue);
	///���ýڵ�����
	long SetAttribute(const char* cstrBaseKeyName, const char* cstrValueName,
					const char* cstrAttributeName, const char* cstrAttributeValue);

	std::string GetNodeAttribute(MSXML2::IXMLDOMNodePtr &foundNode, const char* cstrAttributeName, const char* cstrDefaultAttributeValue);

	///�õ��ڵ�ֵ
	long GetNodeValue(const char* cstrBaseKeyName, const char* cstrValueName, 
		const char* cstrDefaultValue, std::string& strValue, const char* cstrAttributeName, 
		const char* cstrDefaultAttributeValue,std::string& strAttributeValue);
	
	///���ýڵ�ֵ
	long SetNodeValue(const char* cstrBaseKeyName, const char* cstrValueName, 
		const char* cstrValue=NULL, const char* cstrAttributeName=NULL,
		const char* cstrAttributeValue=NULL);

	///ɾ��ĳ�ڵ���������ӽڵ�
	/*!
      �����ӽڵ�ļ�ֵ���浽����keys_val��.
    */
	long DeleteSetting(const char* cstrBaseKeyName, const char* cstrValueName);

	///�õ�ĳ�ڵ���ӽڵ�ļ���
	/*!
      �����ӽڵ�ļ������浽����keys_val��.
    */
	long GetKeysValue(const char* cstrBaseKeyName, 
		std::map<std::string, std::string>& keys_val);

	///�õ�ĳ�ڵ���ӽڵ�ļ���
	long GetKeys(const char* cstrBaseKeyName, 
		std::vector<std::string>& keys);

	///����XML�ļ�
	/** 
		\param filename �����ļ���.
	*/
	BOOL save(const char* filename=NULL);
	
	///װ��XML�ļ�
	/** 
		\param filename װ���ļ���.
	*/
	int load(const char* filename, const char* root_name="xmlRoot");

	///������ı�
	void DiscardChanges();
	///�������
	void clear(const char* root_name="xmlRoot");

	//------------------------------------------------------------------------------------
	long GetRootElem(MSXML2::IXMLDOMElementPtr rootElem);
	long GetNode(const char* cstrKeyName, MSXML2::IXMLDOMNodePtr& foundNode);
	long GetNodeList(const char* cstrKeyName, MSXML2::IXMLDOMNodeListPtr& foundNodeList);

protected:

	MSXML2::IXMLDOMDocument2Ptr XmlDocPtr;

	std::string xml_file_name, m_root_name;

	std::string* ParseKeys(const char* cstrFullKeyPath, int &iNumKeys);	

	MSXML2::IXMLDOMNodePtr FindNode(MSXML2::IXMLDOMNodePtr parentNode, std::string* pCStrKeys, int iNumKeys, BOOL bAddNodes = FALSE, CString strKeyValue="");

	// add a node given a chain of key names
	MSXML2::IXMLDOMNodePtr AddNode(MSXML2::IXMLDOMNodePtr parentNode, std::string* pCStrKeys, int iNumKeys);

};

#endif // _XMLFILE_H_

// end of file 


