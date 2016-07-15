#if !defined(CDES_H)
#define CDES_H

class CDES
{
public:
		//类构造函数
         CDES(); 

		 //类析构函数
        ~CDES(); 

		CString EncryString(CString strOriginal, CString strKey);
		CString DecryString(CString strCiphertext, CString strKey);

private:
	char ConvertHexChar(char ch);

	 /**
     * @notes 十六进制字符串转化为字节数组
     * @param str
     * @return byte[]
     */
	 bool HexStrToBytes(CString strHexSrc, char *bytes, UINT lenbytes);

	/**
     * @notes 字节数组转化为十六进制字符串
     * @param bytes
     * @return String
     */
	 CString BytesToHexStr(char *bytes, int lenbytes);

};

#endif // !defined(CDES_H)