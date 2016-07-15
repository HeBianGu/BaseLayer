#ifndef _XMLFILE_H_
#define _XMLFILE_H_

#pragma warning(disable: 4786)

#import <msxml3.dll>

#include <atlbase.h>
#include <string>
#include <map>
#include <vector>

//typedef const char* LPCTSTR;

//!  XMLFile class 读写XML文件 
/*!
  可用来保存应用程序设置。
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

	///得到节点值(长整型)
	///保存XML文件
	/** 
		\param cstrBaseKeyName 基键名.
		\param cstrValueName 键名(取值名).
		\param lDefaultValue 默认长整值.
	*/
	long GetLong(const char* cstrBaseKeyName, const char* cstrValueName, long lDefaultValue);
	///设置节点值(长整型)
	/** 
		\param cstrBaseKeyName 基键名.
		\param cstrValueName 键名(取保存值名).
		\param lDefaultValue 默认长整值.
	*/
	long SetLong(const char* cstrBaseKeyName, const char* cstrValueName, long lValue);

	///得到节点值(字符串)
	/** 
		\param cstrBaseKeyName 基键名.
		\param cstrValueName 键名(取值名).
		\param cstrDefaultValue 默认字符串值.
	*/
	std::string GetString(const char* cstrBaseKeyName, const char* cstrValueName, const char* cstrDefaultValue);
	///设置节点值(字符串)
	/** 
		\param cstrBaseKeyName 基键名.
		\param cstrValueName 键名(保存值名).
		\param cstrDefaultValue 默认字符串值.
	*/
	long SetString(const char* cstrBaseKeyName, const char* cstrValueName, const char* cstrValue);
	
	///得到节点属性
	/** 
		\param cstrBaseKeyName 基键名.
		\param cstrValueName 键名(保存属性键名).
		\param cstrAttributeName 属性名(保存属性值名).
		\param cstrDefaultAttributeValue 默认属性值.
	*/
	std::string GetAttribute(const char* cstrBaseKeyName, const char* cstrValueName, 
		const char* cstrAttributeName, const char* cstrDefaultAttributeValue);
	///设置节点属性
	long SetAttribute(const char* cstrBaseKeyName, const char* cstrValueName,
					const char* cstrAttributeName, const char* cstrAttributeValue);

	std::string GetNodeAttribute(MSXML2::IXMLDOMNodePtr &foundNode, const char* cstrAttributeName, const char* cstrDefaultAttributeValue);

	///得到节点值
	long GetNodeValue(const char* cstrBaseKeyName, const char* cstrValueName, 
		const char* cstrDefaultValue, std::string& strValue, const char* cstrAttributeName, 
		const char* cstrDefaultAttributeValue,std::string& strAttributeValue);
	
	///设置节点值
	long SetNodeValue(const char* cstrBaseKeyName, const char* cstrValueName, 
		const char* cstrValue=NULL, const char* cstrAttributeName=NULL,
		const char* cstrAttributeValue=NULL);

	///删除某节点和其所有子节点
	/*!
      所有子节点的键值保存到参数keys_val里.
    */
	long DeleteSetting(const char* cstrBaseKeyName, const char* cstrValueName);

	///得到某节点的子节点的键名
	/*!
      所有子节点的键名保存到参数keys_val里.
    */
	long GetKeysValue(const char* cstrBaseKeyName, 
		std::map<std::string, std::string>& keys_val);

	///得到某节点的子节点的键名
	long GetKeys(const char* cstrBaseKeyName, 
		std::vector<std::string>& keys);

	///保存XML文件
	/** 
		\param filename 保存文件名.
	*/
	BOOL save(const char* filename=NULL);
	
	///装载XML文件
	/** 
		\param filename 装入文件名.
	*/
	int load(const char* filename, const char* root_name="xmlRoot");

	///不保存改变
	void DiscardChanges();
	///清空内容
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


