var langStr = 'ch';
var ucNewPrice = "$49.95";
var ucUpgradePrice = "$24.95";
var ueUcBundleNewPrice = "$89.95";      //new bundle cost
var ueUcBundleRetailPrice = "$109.90";   //bundle retail price
var ueUCBundleYouSavePercentage = "40% on UC";
var ueUcUpgradeBundlePrice = "$44.95";    // bundle upgrade price
var ueUpgradeToUeUcBundlePrice = "$59.95";
var strReminderText = 'ʣ������: ';

var strTopRightBlockContent = 
    '<p class="register_title">Register</p>' +
    '<p><img src="images/ucbox_sm.gif" alt="UltraCompare" border="0" class="ucbox" />ע��: <br /><span class="price" id="ucNewPrice">$##.##</span></p>' +
    '<a href="#" onClick="purchaseLink(\'BuyUC\', langStr); return false;" target="_blank"><p class="buy_button">����</p></a>' +
    '<p class="upgrade_text">����: <span class="price" id="ucUpgradePrice">$##.##</span></p>' +
    '<a href="#" onClick="purchaseLink(\'UpgradeUC\', langStr); return false;" target="_blank"><p class="upgrade_button">����</p></a><br />';
                  
var strBottomRightBlockContent = 
    '<p class="register_title">UE/UC </p>' +
    '<p><img src="images/ue_uc_box.gif" alt="UltraEdit UltraCompare ����" border="0" class="bundlebox" />����ע��: <span class="price" id="ueUcBundleNewPrice">$##.##</span></p>' +
    '<a href="#" onClick="purchaseLink(\'BuyUEUCBundle\', langStr); return false;" target="_blank"><p class="buy_button">����</p></a>'+
    '<p class="save_on_uc_text">Save 40% on UC!</p> ';

var strHeaderSubText= 
    '<h2>�����û��ע�������� </h2>' +
    '<p>�����������ð汾��ʹ���������� </p> ';
    
var strUpgradeReminderHeaderSubText = 
    '<h2>��������������ѹ�</h2> ' +
    '<p>�����������ɻ������һ������������ͬʱ��ʡ50 ��</p>';

var strStdTopHeadline = 'UltraCompare ���ð汾��ʾ';
var strHurryTopHeadline = '������ð汾��������';
var strExpiredTopHeadline = 'Ŷ... ������ð汾�Ѿ�����';
var strUpgradeReminderTopHeadline = '��Ȩ��������';

                          
// ----- DIALOG CONTENT ---------//     
var defaultContent =
'<h2>лл���� UltraCompare</h2>'+ 
'<p class="subtext">�����������ð汾��ʹ����������</p>'+ 
'<p>��� UltraCompare ��û��ע�ᡣ����������ʹ�������ʽ���빺����Ȩ�� </p>'+ 
'<p><a href="http://www.ultraedit.com/redirects/registration/en/uc_register.html?utm_source=UltraCompare&utm_medium=ipm&utm_campaign=default" target="_blank">������ע��</a></p>'+ 
'<p style="clear: all"><b>���� Visa, MasterCard, Amex</b></p>'+ 
'<p>���뷽�����������Ƽ����޹�˾(IDM �й�)<br />�����к�������������·50��Ժ<br />H���ش���B1��606��, �ʱ� ##.##8<br />����: ##.##-##.##628<br />����: <a href="mailto:sales@ultraedit.cn" target="_blank">sales@ultraedit.cn</a></p>'+ 
'<p><a href="http://www.ultraedit.com/company/contact_us.html?utm_source=ultracompare&utm_medium=ipm&utm_campaign=default" target="_blank">��������⣬�����������</p>';
                     
var welcomeContent =
		'<h2>лл������ UltraCompare</h2>'+
		'<p>���� IDM �û�������Դ����������������� - ����֧�֡����ء����ŵ�...</p>'+
		'<div class="powertips">'+
		'<p><span class="resource_title">������ʾ/�̳�</span><br />'+
		'"How-tos" �Ǹ����°汾�Ĺ��ܣ������й��ܵ�����̡̳�</p>'+
		'</div>'+
		'<div class="tech_support">'+
		'<p><span class="resource_title">����֧��</span><br />'+
		'������?  �д�!  �������ļ�����Դ�� IDM ���Ϊ������</p>'+
		'</div>'+
		'<div class="forums">'+
		'<p><span class="resource_title">�û����û���̳</span><br />'+
		'�μ���̳�� IDM ��Ʒ�İ����߷��� - �����ɣ��õ�����ȡ�</p>'+
		'</div>'+
		'<p class="resource_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/resources.html\'); return false;" target="_blank">������Դ����</a></p>';
    
var textModeContent = 
    '<img src="images/text_compare_icon.jpg" alt="�ı��Ƚ�" border="0" class="tip_screenshot" />'+
    '<h2>�ı��Ƚ�</h2>'+
    '<p class="subtext">�Ƚ��ı��ļ���Դ���롢 Word �ļ�\'s �͸���...</p>'+
    '<p>UltraCompare ֧��2����3���Ƚ� - ���ڰ汾���ơ���֤���ݺ�׷����Ķ���ĸ�����</p>' +
    '<p style="clear: both">����ϵ�������󻯵ذ��ļ��ķֱ���������������������׵�ʶ��ͺϲ��ļ��ķֱ�' +
    '<p>UltraCompare ͻ�����ı���ÿһ���ַ��ķֱ������󻯵��ļ��ֱ�����졣</p>';
    
    
var folderModeContent = 
    '<img src="images/folder_compare_icon.jpg" alt="Ŀ¼�Ƚ�" border="0" class="tip_screenshot" />'+
    '<h2>Ŀ¼�Ƚ�</h2>'+
    '<p class="subtext">�Ƚ����Ŀ¼�� .zip �����͸����й�Ŀ¼��ʽ</p>' +
    '<p>����Ŀ¼��ʽ�ܿ�رȽϱ��ء������Զ��Ŀ¼ (�������õ� FTP/SFTP ֧��)�� </p>'+
    '<p style="clear: both">���Ŀ¼�ṹ?  ��������... Ŀ¼��ʽ�Ǹ���Ŀ¼�ṹ����û����ܣ����㿪ʼ�����߼���Ŀ¼��ͬʱ UltraCompare �����ڱ���������Ŀ¼��</p>'+
    '<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/recursive_compare.html\'); return false;" target="_blank">�������˽����</a></p>';

var mergeContent = 
    '<img src="images/merge_icon.jpg" alt="�ϲ�" border="0" class="tip_screenshot" />'+
    '<h2>�ϲ��ֱ�</h2>'+
    '<p class="subtext">���ÿ���з�ʽ�ϲ��ϲ��ֱ�</p>'+
    '<p>UltraCompare ��ǿ��ĺϲ�ѡ��������ڱȽ��ļ��ֱ�����ȫ���ơ�</p>'+
    '<p>UltraCompare רҵ��ǿ���ֱ���Ĺ��ܽ�ʡ��ܶ�ʱ�䡣��������úϲ����������ڱȽϴ����еıȽ�����Ƚϲ˵�ѡ����ϲ��ֱ� </p>'+
    '<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/block_line_mode_merge.html\'); return false;" target="_blank">�������˽����</a></p>';
    
var defaultContent = 
    '<h2>лл���� UltraCompare</h2>'+ 
    '<p class="subtext">�����������ð汾��ʹ����������</p>'+ 
    '<p>��� UltraCompare ��û��ע�ᡣ����������ʹ�������ʽ���빺����Ȩ�� </p>'+ 
    '<p><a href="#" onClick="purchaseLink(\'BuyUC\', langStr); return false;" target="_blank">������ע�� UltraCompare</a></p>'+ 
    '<p style="clear: all"><b>���� Visa, MasterCard, Amex</b></p>'+ 
    '<p>IDM Computer Solutions, Inc.<br />5559 Eureka Dr. Ste. B<br />Hamilton, OH ##.##<br />Fax: (513) 8##.##00<br />Email: <a href="mailto:idm@idmcomp.com" target="_blank">idm@idmcomp.com</a></p>'+ 
    '<p><a href="#" onClick="openLink(\'http://www.ultraedit.com/company/contact_us.html\'); return false;" target="_blank">��������⣬�����������</a> ';
    
var benefitsContent =
    '<h2>ע�˲�ĺô�</h2>'+ 
    '<div class="benefits">'+ 
    '<p><span class="subtext">Ϊ���������� - ����֧��</span><br />'+ 
    '���缶�ļ���֧�� - ��׼��Ӧʱ��: 30 ���ӻ���١�</p>'+ 
    '<p><span class="subtext">һ���������</span><br />'+ 
    'ע��������һ���������������Ҫ�ʹ�Ҫ�İ汾��</p>'+ 
    '<p><span class="subtext">�����ô�</span><br />'+ 
    '�����������û����ḻ������Դ��������ʾ���û���������ģʽ���¿�����̳�� </p>'+ 
//    '<div class="bonus">'+       
//      '<p><span class="subtext">�û���������Ȩ!</span><br />'+ 
//      'ӵ��ϥ���������Ի���� PC? ע����������㰲װ�ڶ�������� - ����������Ψһ��������û���</p>'+ 
//    '</div>'+ 
    '</div> <!-- end benefits -->';
    
var editTextContent =
    '<img src="images/edit_text_icon.jpg" alt="Edit Text" border="0" class="tip_screenshot" />'+ 
    '<h2>�༭�ı�</h2>'+ 
    '<p class="subtext">�ڱȽϴ�����ֱ�ӱ༭�ı�</p>'+ 
    '<p style="clear: both">���˱ȽϷֱ���ʱ������Ҫ������... ����Ҫֱ�ӱ༭�ļ�!  �༭�ļ����������Ĺ���Ч���Ǻ���Ҫ�ġ�</p>'+ 
    '<p>�ı��Ƚϸ������������༭�ļ�: �ڱȽϴ���ֱ�ӱ༭����Ӧ�ó�����·��Ļ�д����б༭���ڴ�����ֱ�ӱ༭���������ͼ���... ���Ǻ�����!</p>'+ 
    '<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/editing_files.html\'); return false;" target="_blank">�������˽����</a></p>';    

var sessionsContent =
    '<img src="images/sessions_icon.gif" alt="�Ի�" border="0" class="tip_screenshot" />'+ 
    '<h2>Sessions</h2>'+ 
    '<p class="subtext">�öԻ��������Ƚ����</p>'+ 
    '<p>�����Ӧ�ó�����㹤����Ӱ�죬���� UltraCompare ����Ի�������򻯱Ƚ�!</p>'+ 
    '<p>�Ի�����������Ƚ���� - ���ۺη�ʽ - ȫ��һ��Ӧ�ó��� ��ǩ�����������׵��л�����Ի���</p>'+ 
    '<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/ultracompare_sessions.html\'); return false;" target="_blank">�������˽����</a></p>';

var webCompareContent = 
    '<img src="images/web_compare_icon.gif" alt="���Ƚ�" border="0" class="tip_screenshot" />'+ 
    '<h2>Compare Web Files</h2>'+ 
    '<p class="subtext">�Ƚϱ��غ�Զ���ļ�</p>'+ 
    '<p>����㴦�����ļ���������Ѿ�ϰ����ͨ�� FTP �����ļ���ۿ���Դ�������ı���Ȼ��Ƚϡ�</p>'+
    '<p style="padding-top: 3px">����ͬ�� - һ���и����׵ķ���!</p> '+
    '<p>�������Ƚϣ����ļ�;�������� URL��Ȼ��ȥ...  ����! ���Ƚϳ�������Ĳ�����������ļ���������һ����ıȽϡ�</p> '+
    '<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/web_compare.html\'); return false;" target="_blank">�������˽����</a></p>';
    
    
    
var wordDocCompare = 
'<img src="images/word_compare.gif" alt="Word �Ƚ�" border="0" class="tip_screenshot" />'+ 
'<h2>Word Doc/RTF �Ƚ�</h2>'+ 
'<p class="subtext">�Ƚ� Word Docs �� RTF �ļ�</p>'+ 
'<p>����һ�� Word �ļ���������Э����? ���׵ظ������еĸı䡣</p>'+ 
'<p style="clear: both">UltraCompare רҵ��֧�� Microsoft �� RTF �ļ��ıȽϺͺϲ����������֪�������ĸı䣬UltraCompare\ �� Word Doc �Ƚ��������!</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/word_doc_compare.html\'); return false;" target="_blank">�������˽����</a></p>';

var folderModeFiltersContent = 
'<img src="images/folder_mode_filters_icon.gif" alt="Ŀ¼��ʽ������" border="0" class="tip_screenshot" />'+ 
'<h2>���˱ȽϽ��</h2>'+ 
'<p class="subtext">�򵥻��ȽϽ��</p>'+ 
'<p>�Ƚ�Ŀ¼�ṹ��һ�����ѵĹ������ر������Ŀ¼�����ܶ��ļ�/Ŀ¼�� </p>' +
'<p style="clear: both">����������ý���鹤������ɫ������Ҫ�����ض�������/Ŀ¼��ֻ����ʾ�ض��Ľ�������� "��ͬ�ļ�". </p>'+ 
'<p>UltraCompare �ṩ�������������Ľ�������㽹��Ƚ��顣</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/filtering_files.html\'); return false;" target="_blank">�������˽����</a></p>';

var compareProfilesContent = 
'<img src="images/profiles_icon.gif" alt="���" border="0" class="tip_screenshot" />'+ 
'<h2>�Ƚ����</h2>'+ 
'<p class="subtext">��Ҫ�����ض����ñȽ�ͬһ���ļ���Ŀ¼?</p>'+ 
'<p>�ñȽ���۱����������ıȽ����á�</p>'+ 
'<p style="clear: all">�Ƚ�������㱣��Ƚ����ú������ڻ����Ի��������ǡ� '+
'<p>�Ƚ�������������ơ�����ѡ��������ļ���Ŀ¼·���ȱ������á���������롢����ͷ���������!</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/custom_user_profiles.html\'); return false;" target="_blank">�������˽����</a></p>';

var ignoreOptionsContent = 
'<img src="images/ignore_options_icon.gif" alt="���" border="0" class="tip_screenshot" />'+ 
'<h2>�ļ�����ѡ����</h2>'+ 
'<p class="subtext">�ÿɵ����ĺ���ѡ������Բ���ص��ı�</p>'+ 
'<p>�ܶ����Ա��Ҫ�Ƚ�Դ���������ע��... ����ѡ����������������������ĸ���!</p>'+ 
'<p>���Ƿ�����������һ�����ļ������� tabs��spaces ������ֹ��? ����ѡ��������Ƚ��ļ������ݺͺ��� whitespace����ѡ����������ڲ�ͬ�ֱ�����;���ϡ� </p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/ignore_options.html\'); return false;" target="_blank">�������˽����</a></p>';

var shellIntegrationContent =
'<img src="images/shell_integration_icon.gif" alt="Shell ����" border="0" class="tip_screenshot" />'+ 
'<h2>Shell ����</h2>'+ 
'<p class="subtext">���Ӵ���������һ��˵�Ѹ�ٵ�ִ���ļ���Ŀ¼�Ƚ��ж�</p>'+ 
'<p style="clear: both">������Ǿ����Ƚ��ļ���Ŀ¼������Ҫһ���졢��Ч�ʺ�����ʹ�á�UltraCompare ���� Shell �����Ӵ����������Ƚ��ж���˳��</p>'+ 
'<p>Shell �����������Ӵ����������ļ���ִ�бȽ� - ȫ���ӷ�����һ��˵���</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/ultracompare_shell_integration.html\'); return false;" target="_blank">�������˽����</a></p>';

var ftpCompareContent = 
'<img src="images/compare_ftp_folders.gif" alt="FTP Compare" border="0" class="tip_screenshot" />'+ 
'<h2>FTP/SFTP ֧Ԯ - Զ���ļ���Ŀ¼�Ƚ�</h2>'+ 
'<p class="subtext">������ FTP/SFTP ֧Ԯ�ȽϺ�ͬ��Զ���ļ���Ŀ¼</p>'+ 
'<p>UltraCompare\ �� FTP/SFTP Ŀ¼�Ƚ���������ڷ�������Զ���ļ���Ŀ¼��ִ��Ŀ¼�ȽϺ�ͬ�����Զ���ļ���Ŀ¼���Ƚϡ��ϲ��ͱ���... ������! </p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/compare_FTP_directories.html\'); return false;" target="_blank">�������˽����</a></p>';

var favoriteFilesAndFoldersContent = 
'<img src="images/favorite_files_folders_icon.gif" alt="Favorites" border="0" class="tip_screenshot" />'+ 
'<h2>ϲ���ļ�Ŀ¼</h2>'+ 
'<p class="subtext">����ϲ��������ʱ���õ��ļ���Ŀ¼</p>'+ 
'<p>���Ƿ񾭳��Ƚ�ͬһ���ļ���Ŀ¼? ����ǣ�����Ҫϲ��!</p>'+ 
'<p style="clear: both">ϲ����һ���ܷ����;������ǩ�㾭���õ��ļ���Ŀ¼���Ƚϵ�ʱ��ͻ���졣</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/bookmark_favorite_files-folders_in_ultracompare.html\'); return false;" target="_blank">�������˽����</a></p>';

var snippetCompareContent =
'<img src="images/compare_text_snippets_icon.gif" alt="Favorites" border="0" class="tip_screenshot" />'+ 
'<h2>�Ƚ��ı�ժ¼</h2>'+ 
'<p class="subtext">����Ҫ��/�����ļ������Ƚ��ı�ժ¼</p> '+
'<p>ֻ�ǿ���/ճ������ı����ȽϿ�! </p>'+ 
'<p style="clear: both">����ԴӺܶ�;������/ճ���ı���������ʡ�Word Documents����ҳ�ȡ����ϲ���ı�ժ¼�ȣ����������Ĺ���Ч�ʡ�</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/compare_code_snippets.html\'); return false;" target="_blank">�������˽����</a></p>';

var archiveCompareContent =
'<img src="images/zip_archive_icon.gif" alt="Archive Compare" border="0" class="tip_screenshot" />'+ 
'<h2>�浵�Ƚ�</h2>'+ 
'<p class="subtext">�ô浵�ȽϱȽ� .zip��.jar�� .rar �浵</p>'+ 
'<p>�д浵?  UltraCompare �Ĵ浵�Ƚ�����Ƚ�.zip��.rar �� Java .jar �浵�����ݡ�</p>'+ 
'<p>�浵�Ƚϲ�����֧�����ϵ��ܻ�ӭ�ĸ��⣬��Ҳ��֧�����뱣���� .zip �浵!</p>'+ 
'<p>���ô浵�ȽϺͼ�����ļ�ϵͳ�ϵĴ浵��Ŀ¼���浵�ȽϺ�����-to-use!</p>';

var browserViewContent =
'<img src="images/web_compare_icon.gif" alt="���Ƚ�" border="0" class="tip_screenshot" />'+ 
'<h2>HTML �������ͼ</h2>'+ 
'<p class="subtext">����HTML Ԥ�������󻯵ؼ�����Դ����</p>'+ 
'<p>�����Ƚ�/�ϲ� HTML �ļ�������Ҫ������������仯�������ҵ��˶Եĵط�!'+ 
'<p style="clear: both; padding-top: 5px">UltraCompare ֧���ڱȽϿ���ۺ��������������Ԥ�� ������ HTML���Ƚϡ��ϲ���/��༭��Ȼ�����������ͼ���п�����󻯵ı仯��顣</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/visually_inspect_HTML.html\'); return false;" target="_blank">�������˽����</a></p>';

var customColorsContent =
'<img src="images/custom_colors_icon.gif" alt="�Զ���ɫ" border="0" class="tip_screenshot" />'+ 
'<h2>�Զ���ɫ</h2>'+ 
'<p class="subtext">�Զ��������ɫ</p>'+ 
'<p>�Ƿ���Ҫ�Զ���ɫ������ UltraCompare �÷�? </p>'+ 
'<p style="clear: both">UltraCompare ���������Զ���ɫ��������һ��ϲ������ɫ����? ϰ�������Լ�����ɫ����? ���������ɫ����! </p>'+ 
'<p>���У�����Դ���ͱ�������ɫ������ת�����ס�</p> '+
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/customize_colors.html\'); return false;" target="_blank">�������˽����</a></p>';

var quickDifferenceCheckContent =
'<img src="images/command_line_quick_diff.gif" alt="�Զ���ɫ" border="0" class="tip_screenshot" />'+ 
'<h2>�����зֱ����</h2>'+ 
'<p class="subtext">ִ�������зֱ���飬���ô� UltraCompare �Ƚ�����ļ� </p>'+ 
'<p style="clear: both">�����зֱ��������ļ��ķֱ𣬲�����ʾ GUI - ��Ƚ��ر��!</p>'+ 
'<p>�������Ҫ�������ļ��Ƿ�ͬ������֪�����ǵķֱ��������һ��������!</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/quick_diff_check.html\'); return false;" target="_blank">�������˽����</a></p>';

var manualAlignContent =
'<img src="images/manual_allign_icon.gif" alt="�ֶ�����" border="0" class="tip_screenshot" />'+ 
'<h2>�ֶ�����</h2>'+ 
'<p class="subtext">����Դ�ļ��ֶ����/ͬ����</p>'+ 
'<p>��ʱ������ļ��������Ƶ����ϣ�UC û�з���֪����Ե��С���������Ҫ��һ�������ֶ�ͬ���Ƚϣ���ʾ�ļ���like���Ĳ��֡��ֶ�ͬ���� UC ����������Ϊ��֪��Ҫͬ�����С�ֻ���һ��κ����к�ͬ�������������Ƚϡ�</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/manual_sync.html\'); return false;" target="_blank">�������˽����</a></p>';

var outputContent = 
'<img src="images/report_icon.gif" alt="Output" border="0" class="tip_screenshot" />'+ 
'<h2>Reporting</h2>'+ 
'<p class="subtext">����ͱ�������ʽ�Ľ��</p>'+ 
'<p>����ͱ�������ʽ�Ľ��: �Զ��ԡ� context �ڵ�...</p>'+ 
'<p style="clear: both">�������ܿ쿴���Ƚ��ļ��Ĳ�֮ͬ����ֻ������ Difference Summary ���档  �������Ҫ��ϸ�ı��棬����Բ���һ������ļ����������ѡ����ĶԻ������������ʽ��</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/export_save_text_compare_output.html\'); return false;" target="_blank">�������˽����</a></p>';

var configureTimeDateContent = 
'<img src="images/custom_date_format_icon.gif" alt="�Զ����ڸ�ʽ" border="0" class="tip_screenshot" />'+ 
'<h2>ʱ���������ʾ</h2>'+ 
'<p class="subtext">�Զ�ʱ���������ʾ��ʽ</p>'+ 
'<p>��ִ��Ŀ¼�Ƚϣ�����ܲ�̫����һ�����Сʱ��ϸ�ڣ�����������뿴һ���½����</p>'+ 
'<p>�㲻Ӧÿһ�����Ƚϵ�ʱʱ����ʱ������ڵĸ�ʽ������Ӧ����������UltraCompare �����Զ�ʱ������ڵĸ�ʽ�����ú�������... ����!</p>'+ 
'<p class="powertip_link"><a href="#" onClick="openLink(\'http://www.ultraedit.com/support/tutorials_power_tips/ultracompare/customize_time_date.html\'); return false;" target="_blank">�������˽����</a></p>';



// ----- END DIALOG CONTENT ---------//

// ---- START RIGHT/LEFT CONTENT ----//

var rightSideContent =
    '<img src="images/ue_uc_box_big.gif" alt="UltraEdit UltraCompare Bundle Box" border="0" class="ue_uc_box_big" />'+ 
		'<h2>UE/UC Bundle</h2>'+ 
		'<p class="ue_uc_bundle_offer">���ۼ�: <span class="retail_price" id="ueUcBundleRetailPrice">$##.##</span><br />'+ 
			'���ļ۸�: <span class="price" id="ueUcBundleNewPrice">$##.##</span><br />'+ 
			'�����ۿ�: <span class="you_save" id="ueUCBundleYouSavePercentage">##</span>'+ 
		'</p>'+ 
    '<p style="clear: both">UltraEdit/UltraCompare ��һ����ȫ�ۺϵ������ı��༭������ ���� UE/UC ������ʡ UC �� 40%!</p>'+     
	  '<div class="register_upgrade_container">'+ 
	  '<div class="best_value">��ü۸�</div>' +
	  	'<div class="register">'+ 
				'<p class="register_text">����ע�� <span class="price" id="ueUcBundleNewPrice">$##.##</span></p>'+ 
				'<a href="#" onClick="purchaseLink(\'BuyUEUCBundle\', langStr); return false;" target="_blank"><p class="buy_button">����</p></a>'+ 
			'</div>'+ 
			'<div class="upgrade">'+ 
				'<p class="register_text">UE ���� + UC <span class="price" id="ueUpgradeToUeUcBundlePrice">$##.##</span></p>'+ 
				'<a href="#" onClick="purchaseLink(\'UpgradeUE2UEUCBundle\', langStr); return false;" target="_blank"><p class="buy_button">����</p></a>'+ 
		'</div>';

var leftSideContent =
    '<img src="images/ucbox_big.gif" alt="UltraEdit ��װ" border="0" class="ucbox_big" />'+ 
    '<h2>UltraCompare Pro</h2>'+ 
    '<p>UltraCompare Pro ��һ��ǿ��ıȽ�/�ϲ�Ӧ�ó�������������׷���ļ���Ŀ¼�� .zip �����ķֱ�! '+ 
    '<div class="bonus">'+ 
			  '<p><span class="subtext">һ���������</span><br />'+ 
    		'ע��������һ���������������Ҫ�ʹ�Ҫ�İ汾��</p>'+ 
    '</div>'+ 
    '<div class="register_upgrade_container">'+ 
    '<div class="register">'+ 
    '<p class="register_text">ע�� <span class="price" id="ucNewPrice">$##.##</span></p>'+ 
    '<a href="#" onClick="purchaseLink(\'BuyUC\', langStr); return false;" target="_blank"><p class="buy_button">����</p></a>'+ 
    '</div>'+ 
    '<div class="upgrade">'+ 
    '<p class="register_text">UC ����<span class="price" id="ucUpgradePrice">$##.##</span></p>'+ 
    '<a href="#" onClick="purchaseLink(\'UpgradeUC\', langStr); return false;" target="_blank"><p class="buy_button">����</p></a>'+ 
    '</div>';


// ---- END RIGHT/LEFT CONTENT ----//



// ---- START RIGHT/LEFT CONTENT FOR UPGRADE REMINDER MESSAGE----//

var upgradeRightSideContent =
    '<img src="images/ue_uc_box_big.gif" alt="UltraEdit UltraCompare Bundle Box" border="0" class="ue_uc_box_big" />'+ 
		'<h2>UE/UC Bundle</h2>'+ 
		'<p class="ue_uc_bundle_offer">Upgrade both today for<br /> Only: <span class="price" id="ueUcUpgradeBundlePrice">$##.##</span></p>'+ 
		'<p style="clear: both">Using UltraEdit? Save an additional 20% when you upgrade UltraCompare and UltraEdit together, plus receive another year of free upgrades for both products.</p>'+ 
	  '<div class="register_upgrade_container">'+ 
			'<div class="register" style="height: 70px; background-image: url(images/best_value_arrow.gif); background-position: 100% 50%; background-repeat: no-repeat;">'+ 
				'<p style="padding-top: 23px; padding-left: 23px; font-weight: bold">BEST VALUE</p>'+ 
			'</div>'+ 
			'<div class="upgrade">'+ 
				'<p class="register_text">UE/UC Upgrade<span class="price" id="ueUcUpgradeBundlePrice">$##.##</span></p>'+ 
				'<a href="#" onClick="purchaseLink(\'UpgradeUEUCBundle\', langStr); return false;" target="_blank"><p class="buy_button">UPGRADE</p></a>'+ 
		'</div>';

var upgradeLeftSideContent =
    '<img src="images/ucbox_big.gif" alt="UltraCompare Box" border="0" class="ucbox_big" />'+ 
    '<h2>UltraCompare</h2>'+ 
    '<p>���˽�UltraCompare���µĹ��ܣ�  <a href="#" onClick="openLink(\'http://www.ultraedit.com/products/ultracompare/new_feature_tour.html\'); return false;" target="_blank">See </a> what\'s new and improved since your last upgrade.</p>'+ 
    '<div class="bonus_upgrade">'+ 
      '<p><span class="subtext">��������... 1���������</span><br />'+ 
      '���������°汾�����һ��� <b>��ѵ�</b> ��������������Ҫ/��Ҫ�ķ���</p>'+ 
    '</div>'+ 
    '<div class="register_upgrade_container">'+ 
    '<div class="register">'+ 
	    '<p style="text-align: left; padding-top: 4px; padding-left: 5px"><strong>��������</strong><br>'+ 
	    'ע���û��յ�50 ����ѵ� <br>���ۼۣ�</p>'+ 
    '</div>'+ 
    '<div class="upgrade">'+ 
    '<p class="register_text">UC ����<span class="price" id="ucUpgradePrice">$##.##</span></p>'+ 
    '<a href="#" onClick="purchaseLink(\'UpgradeUC\', langStr); return false;" target="_blank"><p class="buy_button">����</p></a>'+ 
    '</div>';


// ---- END RIGHT/LEFT CONTENT FOR UPGRADE REMINDER MESSAGE----//

