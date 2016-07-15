#if !defined(CDES_H)
#define CDES_H

class CDES
{
public:
		//�๹�캯��
         CDES(); 

		 //����������
        ~CDES(); 

		CString EncryString(CString strOriginal, CString strKey);
		CString DecryString(CString strCiphertext, CString strKey);

private:
	char ConvertHexChar(char ch);

	 /**
     * @notes ʮ�������ַ���ת��Ϊ�ֽ�����
     * @param str
     * @return byte[]
     */
	 bool HexStrToBytes(CString strHexSrc, char *bytes, UINT lenbytes);

	/**
     * @notes �ֽ�����ת��Ϊʮ�������ַ���
     * @param bytes
     * @return String
     */
	 CString BytesToHexStr(char *bytes, int lenbytes);

};

#endif // !defined(CDES_H)