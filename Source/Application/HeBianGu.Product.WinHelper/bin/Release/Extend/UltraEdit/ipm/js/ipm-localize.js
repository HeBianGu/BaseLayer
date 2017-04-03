/*

"loc_langs" - the object holding all items that need localizations

In order for translations to work, the string in HTML source
should be encapsulated in a <span> with a class set to "l_"
PLUS the unique name of the sub-object containing its
translations.

For example, to translate "Install all" in HTML source, find
(or add) that sub-item in the langs object.  It already
exists with a name of "installAll".  Therefore the <span>
surrounding the text in the HTML source should be assigned a
class of "l_installAll". Example:

    <span class="l_installAll">Install all</span>

It's easy enough to add new translated strings.  Simply copy
one of the existing below, give it a unique name, and enter
the translated strings.  Then add the "l_" + unique name
class to a <span> around the original string in the HTML
source.
*/

loc_supportedLangs = [
"en", // English
"de", // German
"fr", // French
"es", // Spanish
"it", // Italian
"ko", // Korean
"jp", // Japanese
"ch", // Chinese (Simplified)
"cht" // Chinese (Traditional)
];

loc_langs = {

// Prices
//---------------------

// Trial text
//---------------------

  trialModeText: {  // **** NEEDS TRANSLATION ****
    en: '<span class="appname"></span> is running in trial mode.',
    de: '<span class="appname"></span> wird im Testmodus ausgeführt',
    fr: '<span class="appname"></span>\'s exécute en mode d’essai.',
    es: '<span class="appname"></span> está ejecutándose en modo de prueba',
    it: '<span class="appname"></span> è in esecuzione in modalità di valutazione ',
    ko: '<span class="appname"></span> - 평가판 모드에서 구동 중입니다.',
    jp: '<span class="appname"></span> は体験版として動作しています',
    ch: '<span class="appname"></span> 运行的是试用模式。',
    cht: '<span class="appname"></span> 正在試用模式下執行'
  },

  trialDaysText: {
    en: 'Days left in free trial:',
    de: 'Verbleibende Tage:',
    fr: 'Jours restants :',
    es: 'Días restantes:',
    it: 'Giorni rimanenti:',
    ko: '남은 기간:',
    jp: '残り日数:',
    ch: '剩余日子:',
    cht: '剩餘天數：'
  },

  trialExpiring: {  // **** NEEDS TRANSLATION ****
    en: 'Your free trial is expiring.',
    de: 'Ihre kostenlose Testversion läuft ab.',
    fr: 'Votre essai gratuit arrive à expiration.',
    es: 'Su versión de prueba está a punto de caducar.',
    it: 'La versione di valutazione sta per scadere. ',
    ko: '무료 평가판이 만료됩니다.',
    jp: '無料の試用期間が間もなく終了します。',
    ch: '您的免费试用期即将到期。',
    cht: '您的免費試用期即將過期。'
  },

  trialAlmostExpiredText: {
    en: 'Your free trial has almost expired.',
    de: 'Die Probezeit ist fast abgelaufen.',
    fr: 'La p&eacute;riode d\'essai expire bient&ocirc;t.',
    es: 'Periodo de prueba finalizando.',
    it: 'La trial e\' quasi scaduta.',
    ko: '시험사용기간이 얼마 남지 않았습니다.',
    jp: 'あと少しで試用期間が切れます',
    ch: '您的试用版本已经过期了。',
    cht: '你的試用差不多要結束了'
  },

  trialExpiredText: {
    en: 'Your free trial has expired.',
    de: 'Die Probezeit ist vorbei.',
    fr: 'Votre p&eacute;riode d\'essai a expir&eacute;.',
    es: 'Fin del periodo de prueba.',
    it: 'La trial e\' scaduta.',
    ko: '시험사용기간이 종료되었습니다.',
    jp: '試用期間が切れました',
    ch: '您的试用版本已经期满了。',
    cht: '你的試用已到期'
  },

  noTrialDaysText: {  // **** NEEDS TRANSLATION ****
    en: 'Please purchase a license to continue using <span class="appname"></span>.',
    de: 'Bitte kaufen Sie eine Lizenz, um <span class="appname"></span> weiterhin benutzen zu können.',
    fr: 'Veuillez acheter la licence pour continuer à utiliser <span class="appname"></span>.',
    es: 'Compre una licencia para seguir utilizando <span class="appname"></span>.',
    it: 'Acquistare una licenza per continuare a utilizzare <span class="appname"></span>. ',
    ko: '<span class="appname"></span> 애플리케이션을 계속 사용하려면 라이센스를 구입하십시오.',
    jp: '<span class="appname"></span>を継続して使用するには、ライセンスを購入してください。',
    ch: '请购买许可证以继续使用 <span class="appname"></span>。',
    cht: '請購買授權以繼續使用<span class="appname"></span>。'
  },

// UE benefits text
//---------------------

  bftHead: {
    en: 'Your personal license includes:',
    de: 'Ihre persönliche Lizenz:',
    fr: 'Votre licence personnelle:',
    es: 'Su licencia personal:',
    it: 'La tua licenza personale:',
    ko: '귀하의 개인 라이센스는:',
    jp: 'ご自身のの個人ライセンス:',
    ch: '你的个人许可证::',
    cht: '你的個人許可證:'
  },

  bftMultiPlatform: {
    en: 'The <strong>Windows</strong>, <strong>Mac</strong>, and <strong>Linux</strong> versions',
    de: 'Enthält Versionen für <strong>Win</strong>, <strong>Mac</strong> und <strong>Linux</strong>',
    fr: '<span style="font-size: 80%;">Comprend les versions <strong>Windows</strong>, <strong>Mac</strong> et <strong>Linux</strong></span>',
    es: 'Incluye versiones para <strong>Win</strong>, <strong>Mac</strong> y <strong>Linux</strong>',
    it: '<span style="font-size: 80%;">Include versioni <strong>Win</strong>, <strong>Mac</strong> e <strong>Linux</strong></span>',
    ko: '<strong>Win</strong>, <strong>Mac</strong>, <strong>Linux</strong> 버전이 포함됩니다',
    jp: '<span style="font-size: 80%;">それには<strong>Win</strong>、<strong>Mac</strong>、<strong>Linux</strong>バージョンがあります</span>',
    ch: '包括 <strong>Win</strong>、<strong>Mac</strong> 和 <strong>Linux</strong> 版本',
    cht: '包括 <strong>Win</strong>、<strong>Mac</strong> 和 <strong>Linux</strong> 版本'
  },

  bftThreeInstalls: {  // **** NEEDS TRANSLATION ****
    en: 'Installation on up to 3 machines',
    de: 'Installation auf bis zu 3 Maschinen',
    fr: '<span style="font-size: 80%;">Installation sur trois ordinateurs</span>',
    es: 'Instalación en hasta 3 equipos',
    it: '<span style="font-size: 70%;">Installazione consentita su un massimo di 3 macchine</span>',
    ko: '최대 3대까지 설치',
    jp: '最大で3台の機器にインストール可能',
    ch: '最多可在 3 台计算机上安装',
    cht: '可在最多 3 部機器上安裝'
  },

  bftFreeUpgrades: {
    en: 'Free upgrades for a year',
    de: 'Bietet ein Jahr lang kostenlose Upgrades',
    fr: '<span style="font-size: 75%;">S\'accompagne d\'une année de mises à jour gratuite</span>',
    es: 'Incluye 1 año de actualizaciones gratuitas',
    it: '<span style="font-size: 80%;">In dotazione aggiornamento gratuiti per 1 anno</span>',
    ko: '1년 무상 업그레이드를 제공합니다',
    jp: '1年間無料アップグレード付きです',
    ch: '随附一年免费升级',
    cht: '隨附一年免費升級'
  },

  bftTechSupport: {  // **** NEEDS TRANSLATION ****
    en: 'Unlimited lifetime tech support',
    de: 'Zeitlich unbegrenzter technischer Support',
    fr: '<span style="font-size: 80%;">Soutien tech. illimité pour la durée de vie du produit</span>',
    es: 'Soporte técnico ilimitado de por vida',
    it: '<span style="font-size: 80%;">Assistenza tecnica illimitata a vita</span>',
    ko: '평생 무제한 기술 지원',
    jp: '生涯無制限のテクニカルサポート',
    ch: '无限制的终身技术支持',
    cht: '無限制的終身技術支援'
  },

  worldsBest: {
    en: 'The world&#39;s #1 text editor.<br>Preferred by millions!',
    de: 'Der weltweit beliebteste Text-Editor.<br>Von Millionen bevorzugt!',
    fr: 'Numéro 1 mondial des éditeurs de texte.<br>L\'éditeur préféré de millions d\'utilisateurs !',
    es: 'El editor de texto nº 1 del mundo.<br>¡Preferido por millones de usuarios!',
    it: 'L\'editor di testo n. 1 al mondo.<br>Preferita da milioni di utenti!',
    ko: '세계 최고의 텍스트 편집기로서.<br>수백만 명이 사용하고 있습니다!',
    jp: '世界でNo.1のテキストエディタ.<br>多くの人のお好みです！',
    ch: '全球一流的文本编辑器.<br>数百万人的首选！',
    cht: '全球一流的文本編輯器.<br>數百萬人的首選'
  },

// Static license/purchase
// buttons and text
//---------------------

  licenseApp: {  // **** NEEDS TRANSLATION ****
    en: 'License <span class="appname"></span>',  // Translation note: this is the verb form of "license"
    de: 'Lizenz <span class="appname"></span>',
    fr: '<span style="font-size: 80%;">Délivrer une licence pour <span class="appname"></span></span>',
    es: '<span style="font-size: 85%;">Obtener licencia para <span class="appname"></span></span>',
    it: 'Licenza <span class="appname"></span> ',
    ko: '<span class="appname"></span> 라이센스 구입',
    jp: '<span class="appname"></span> のライセンス ',
    ch: '授权 <span class="appname"></span>',
    cht: '授權<span class="appname"></span>'
  },

  buyNew: {  // **** NEEDS TRANSLATION ****
    en: 'Buy new:',
    de: 'Neu kaufen:',
    fr: 'Acheter neuf :',
    es: 'Comprar nueva:',
    it: 'Acquista nuova: ',
    ko: '새로 구매:',
    jp: '新しく購入する：',
    ch: '购买新产品：',
    cht: '購買新的：'
  },

  upgrade: {  // **** NEEDS TRANSLATION ****
    en: 'Upgrade:',    // Translation note: this is the verb form of "upgrade"
    de: 'Upgrade:',
    fr: 'Mettre à niveau :',
    es: 'Actualizar:',
    it: 'Aggiorna: ',
    ko: '업그레이드:',
    jp: 'アップグレード：',
    ch: '升级：',
    cht: '升級：'
  },

  phoneOrder: {  // **** NEEDS TRANSLATION ****
    en: 'You can also call +1-513-892-8600 to order over the phone.',
    de: 'Unter +1-513-892-8600 sind auch telefonische Bestellungen möglich.',
    fr: 'Vous pouvez également appeler le  +1-513-892-8600 pour commander par téléphone.',
    es: 'También puede llamar al número +1-513-892-8600 para hacer el pedido por teléfono.',
    it: 'È anche possibile effettuare l\'ordine per telefono, chiamando il numero<br>+1-513-892-8600. ',
    ko: '전화로 주문하는 경우 +1-513-892-8600번을 이용해 주십시오.',
    jp: '電話（+1-513-892-8600）から注文することもできます。',
    ch: '您也可以通过电话拨打 +1-513-892-8600 订购。',
    cht: '您也可以致電：+1-513-892-8600 以透過電話訂購。'
  },

  enterLicenseKey: {  // **** NEEDS TRANSLATION ****
    en: 'Enter license key',
    de: 'Lizenzschlüssel eingeben',
    fr: 'Saisir la clé de licence',
    es: 'Introducir clave de licencia',
    it: 'Inserire chiave di licenza ',
    ko: '라이센스 키 입력',
    jp: 'ライセンス番号を入力してください',
    ch: '输入许可证密钥',
    cht: '輸入授權金鑰'
  },

  support: {  // **** NEEDS TRANSLATION ****
    en: 'Resources and support',
    de: 'Support',
    fr: 'Assistance',
    es: 'Soporte',
    it: 'Risorse ed assistenza ',
    ko: '리소스 및 지원',
    jp: 'リソースとサポート',
    ch: '资源和支持',
    cht: '資源與支援'
  },

  version: {  // **** NEEDS TRANSLATION ****
    en: 'Version info',
    de: 'Versionsinformationen',
    fr: 'Informations sur la version',
    es: 'Información de versión',
    it: 'Info versione',
    ko: '버전 정보',
    jp: 'バージョン情報',
    ch: '版本信息',
    cht: '版本資訊'
  },

// UE/UC bundle text
//---------------------

  bundleHeadline: {
    en: '<strong>Best value:</strong> <span class="appname"></span> / <span class="bdlname"></span> bundle',
    de: '<strong>Bester Preis:</strong> <span class="appname"></span> / <span class="bdlname"></span>',
    fr: '<strong>Le meilleur :</strong> <span class="appname"></span> / <span class="bdlname"></span>',
    es: '<strong>Mejor valor:</strong> <span class="appname"></span> / <span class="bdlname"></span>',
    it: '<strong>Miglior prezzo:</strong> <span class="appname"></span> / <span class="bdlname"></span>',
    ko: '<strong>최상의 가격:</strong> <span class="appname"></span> / <span class="bdlname"></span>',
    jp: '<strong>お買い得:</strong> <span class="appname"></span> / <span class="bdlname"></span>',
    ch: '<strong>最好价格 :</strong>  <span class="appname"></span> / <span class="bdlname"></span>',
    cht: '<strong>最優惠的價格 :</strong> <span class="appname"></span> / <span class="bdlname"></span>'
  },

  ueucPriceRetailText: {
    en: 'Retail:',
    de: '<span style="font-size: 75%;">Normalpreis:</span>',
    fr: '<span style="font-size: 75%;">Vente au d&eacute;tail :</span>',
    es: 'Precio:',
    it: 'Prezzo:',
    ko: '소비자가:',
    jp: '小売価格:',
    ch: '零售价:',
    cht: '零售：'
  },

  ueucPriceText: {
    en: 'You pay:',
    de: 'Sie zahlen:',
    fr: '<span style="font-size: 75%;">Vous payez :</span>',
    es: 'Usted Paga:',
    it: 'Paghi:',
    ko: '할인가:',
    jp: '特別価格:',
    ch: '您的价格:',
    cht: '需付款項：'
  },

  ueucBft1: {  // **** NEEDS TRANSLATION ****
    en: 'Integrated with <span class="appabbr"></span>',
    de: 'Integriert mit <span class="appabbr"></span>',
    fr: 'Intégré à <span class="appabbr"></span>',
    es: 'Integrado con <span class="appabbr"></span>',
    it: 'Integrata con <span class="appabbr"></span>',
    ko: '<span class="appabbr"></span>와 통합',
    jp: '<span class="appabbr"></span>の統合',
    ch: '与 <span class="appabbr"></span> 整合',
    cht: '與 <span class="appabbr"></span> 整合'
  },

  ueucBft2: {  // **** NEEDS TRANSLATION ****
    en: 'Integrate with version control',
    de: 'Mit Versionskontrolle',
    fr: 'Intégrer au contrôle de version',
    es: 'Y con control de versiones',
    it: 'Integrata con controllo versione ',
    ko: '버전 관리와 통합',
    jp: 'バージョン管理機能の統合',
    ch: '与版本控制整合',
    cht: '與版本控制整合'
  },

  ueucBft3: {  // **** NEEDS TRANSLATION ****
    en: 'Diff up to 3 files/folders',
    de: 'Diff für bis zu 3 Dateien/Ordner',
    fr: 'Différentiel sur 3 fich./dossiers',
    es: 'Hasta 3 archivos/carpetas',
    it: 'Diff fino a 3 file/cartelle ',
    ko: '최대 3개 파일/폴더 비교',
    jp: ' 3つのファイル/フォルダ', //original: 3つのファイル/フォルダまで差分可能
    ch: '可区分最多 3 个文件/文件夹',
    cht: '可區分最多 3 個檔案/資料夾'
  },

  ueucBft4: {  // **** NEEDS TRANSLATION ****
    en: 'Compare Word docs, PDFs',
    de: 'Doc(x) und PDFs vergleichen',
    fr: 'Comparer des doc Word, PDF',
    es: 'Comparar Doc(x), PDF',
    it: 'Documenti Word e PDF ',
    ko: 'Word 문서, PDF 비교',
    jp: 'Word文書やPDFファイルを比較',
    ch: '比较 Word 文件、PDF',
    cht: '比較 Word 文件、PDF'
  },

  ueucBft5: {  // **** NEEDS TRANSLATION ****
    en: 'Sync local to FTP',
    de: 'Lokal nach FTP synchronisieren',
    fr: 'Syncrho local avec FTP',
    es: 'Sincronización local en FTP',
    it: 'Sincronizza locale su FTP ',
    ko: '로컬을 FTP에 동기화',
    jp: 'FTPとのローカル同期',
    ch: '同步本机至 FTP',
    cht: '同步本機至 FTP'
  },

  ueucBft6: {  // **** NEEDS TRANSLATION ****
    en: 'Compare & preview HTML',
    de: 'HTML Vergleich und Vorschau',
    fr: 'Comparer/prévisualiser HTML',
    es: 'Comparar y previsualizar HTML',
    it: 'Confronto e anteprima HTML ',
    ko: 'HTML 비교 및 미리보기',
    jp: 'HTMLの比較とプレビュー',
    ch: '比较和预览 HTML',
    cht: '比較與預覽 HTML'
  },

  ueucBft7: {  // **** NEEDS TRANSLATION ****
    en: 'Merge differences',
    de: 'Unterschiede zusammenführen',
    fr: 'Fusionner les différences',
    es: 'Combinar diferencias',
    it: 'Unisci differenze',
    ko: '차이 병합',
    jp: '差分を統合',
    ch: '合并差异',
    cht: '合併差異'
  },

  ueucBft8: {  // **** NEEDS TRANSLATION ****
    en: 'Save compare sessions',
    de: 'Vergleichssitzung speichern',
    fr: 'Sessions de comparaison',
    es: 'Sesiones de comparación',
    it: 'Salva sessioni di confronto',
    ko: '비교 세션 저장',
    jp: 'セッションを比較保存',
    ch: '保存比较会话',
    cht: '儲存比較工作階段'
  },

  ueucSavingsPercent: {  // **** NEEDS TRANSLATION ****
    en: 'Save 50%',
    de: '50 % für <span class="bdlabbr"></span>',
    fr: '<span style="font-size: 90%;">Économisez 50 %</span>',
    es: 'Ahorre un 50%',
    it: 'Salva 50%',
    ko: '<span class="appabbr"></span>과 함께 번들로 구매해서 ',
    jp: '<span class="appabbr"></span>との同時購入で、UCが',
    ch: '您将其与 <span class="appabbr"></span> 绑定时，节省 50%',
    cht: '節省 50%'
  },

  ueucSavingsText: {  // **** NEEDS TRANSLATION ****
    en: 'on <span class="bdlabbr"></span> when you bundle it with <span class="appabbr"></span>!',
    de: 'beim Bündeln mit <span class="appabbr"></span> sparen!',
    fr: 'sur <span class="bdlabbr"></span> lorsque vous le groupez à <span class="appname"></span> !',
    es: 'en <span class="bdlabbr"></span> cuando lo compre junto con <span class="appname"></span>.',
    it: 'su <span class="bdlabbr"></span> quando raggruppate con <span class="appname"></span>!',
    ko: '<span class="bdlabbr"></span> 가격을 50% 절약하세요!',
    jp: '50パーセント引き！',
    ch: '的 <span class="bdlabbr"></span>！',
    cht: '的 <span class="bdlabbr"></span>，當您與 <span class="appabbr"></span> 綑綁時！'
  },

  buyueucNew: {  // **** NEEDS TRANSLATION ****
    en: 'Buy <span class="appabbr"></span>/<span class="bdlabbr"></span> new:',
    de: '<span class="appabbr"></span>/<span class="bdlabbr"></span> neu kaufen:',
    fr: 'Acheter <span class="appabbr"></span>/<span class="bdlabbr"></span> neuf :',
    es: 'Comprar nuevo <span class="appabbr"></span>/<span class="bdlabbr"></span>:',
    it: 'Acquista <span class="appabbr"></span>/<span class="bdlabbr"></span> nuovi:',
    ko: '새 <span class="appabbr"></span>/<span class="bdlabbr"></span> 구매:',
    jp: '<span class="appabbr"></span>/<span class="bdlabbr"></span>を新しく購入：',
    ch: '购买新的 <span class="appabbr"></span>/<span class="bdlabbr"></span>：',
    cht: '購買 <span class="appabbr"></span>/<span class="bdlabbr"></span> 新：'
  },

  upgradeueuc: {  // **** NEEDS TRANSLATION ****
    en: 'Upgrade <span class="appabbr"></span>, add <span class="bdlabbr"></span>:',
    de: '<span class="appabbr"></span> aktualisieren + <span class="bdlabbr"></span>:',
    fr: '<span style="font-size: 75%;">Mettre à niveau <span class="appabbr"></span>, ajouter <span class="bdlabbr"></span> :</span>',
    es: 'Actualizar <span class="appabbr"></span> + <span class="bdlabbr"></span>:',
    it: '<span style="font-size: 80%;">Aggiorna <span class="appabbr"></span>, aggiungi <span class="bdlabbr"></span>:</span>',
    ko: '<span class="appabbr"></span> 업그레이드, <span class="bdlabbr"></span> 추가:',
    jp: '<span style="font-size: 70%;"><span class="appabbr"></span>をアップグレードし、UCを追加：</span>',
    ch: '升级 <span class="appabbr"></span>，新增 <span class="bdlabbr"></span>：',
    cht: '升級 <span class="appabbr"></span>、新增 <span class="bdlabbr"></span>：'
  },

// feature showcase text
//---------------------
  learnMore: {
    en: 'Click to learn more',
    de: 'F&uuml;r weitere Info hier klicken',
    fr: 'Cliquez ici pour en savoir plus',
    es: 'Pulse aquí para saber más',
    it: 'Clicca qui per saperne di più',
    ko: '더 상세한 정보를 알고 싶으면 여기를 누르세요',
    jp: '詳細はここをクリック',
    ch: '单击这里了解更多',
    cht: '點選這裏進一步學習'
  },

  f_MultiCaretHead: {  // **** NEEDS TRANSLATION ****
    en: 'Edit multiple places in your file at once',
    de: 'Gleichzeitig mehrere Stellen in Ihrer Datei bearbeiten',
    fr: 'Modifiez plusieurs endroits en même temps dans votre fichier',
    es: '<span style="font-size: 85%;">Editar en varios lugares del archivo a la vez</span>',
    it: 'Modifica varie posizioni in un file in una volta sola',
    ko: '파일에서 여러 곳을 한 번에 편집할 수 있습니다',
    jp: 'ファイルの複数の場所を一度に編集',
    ch: '在您的文件中一次编辑多个位置',
    cht: '在您的檔案一次編輯多個位置'
  },

  f_MultiCaret: {  // **** NEEDS TRANSLATION ****
    en: 'Press <strong>Ctrl</strong>, then <strong>click</strong> on different places where you want to edit. Begin typing. <span class="appname"></span> updates your file in all locations!<br><br>You can also press <strong>Ctrl</strong> and then <strong>double-click</strong> text or <strong>click and drag</strong> to create multiple selections in the file. Edit, cut, copy, or paste all at once.<br><br><strong>Check this out:</strong> You can also quickly create multiple carets at the end of lines by selecting the lines, then <strong>Ctrl</strong> + <strong>clicking</strong> beyond the end of them.',
    de: '<span style="font-size: 75%;">Drücken Sie <strong>Strg</strong> und <strong>klicken</strong> Sie dann auf verschiedene Stellen, die Sie bearbeiten wollen. Fangen Sie an zu tippen. <span class="appname"></span> aktualisiert alle Stellen Ihrer Datei!<br><br>Sie können auch <strong>Strg</strong> drücken und dann <strong>zweimal</strong> auf den Text klicken oder ihn <strong>anklicken und ziehen</strong>, um mehrere Stellen in der Datei auszuwählen. Sie können gleichzeitig alles bearbeiten, ausschneiden, kopieren oder einfügen.<br><br><strong>Schauen Sie sich das mal an:</strong> Sie können auch schnell mehrere Cursor an Zeilenenden erstellen, indem Sie die Zeilen markieren, die Taste <strong>Strg</strong> drücken und dann hinter die Zeilenenden <strong>klicken</strong>.</span>',
    fr: '<span style="font-size: 90%;">Appuyez sur <strong>Ctrl</strong>, puis <strong>cliquez</strong> là où vous voulez apporter les modifications. Commencez à taper. <span class="appname"></span> actualise les fichiers dans tous les emplacements !<br><br>Vous pouvez également appuyer sur <strong>Ctrl</strong> puis <strong>double-cliquez</strong> sur du texte ou <strong>cliquez et déplacez </strong> pour créer plusieurs sélections dans le fichier Modifiez, couper, copiez et collez en une seule fois.<br><br><strong>Voyez par vous même :</strong> Vous pouvez également créer rapidement plusieurs carets en fin de ligne en sélectionnant les lignes, puis <strong>Ctrl</strong> + et en<strong>cliquant</strong> sur leur droite.</span>',
    es: '<span style="font-size: 85%;">Pulse <strong>Ctrl</strong> y después <strong>haga clic</strong> en los distintos lugares donde desee realizar las modificaciones. Comience a escribir. <span class="appname"></span> actualiza el archivo en todas las ubicaciones.<br><br>También puede presionar <strong>Ctrl</strong> y después <strong>hacer doble clic</strong> en el texto o <strong>hacer clic y arrastrarlo</strong> para crear varias selecciones en el archivo. Edite, corte, copie o pegue todo a la vez.<br><br><strong>Compruebe esto:</strong> Puede crear rápidamente varios símbolos de intercalación seleccionando las líneas y después presionando <strong>Ctrl</strong> y <strong>haciendo clic</strong> después del final de la línea.</span>',
    it: 'Premere <strong>Ctrl</strong>, quindi <strong>cliccare</strong> sulle diverse posizioni che si desidera modificare. In digitazione. <span class="appname"></span> aggiorna il file in tutte le posizioni!<br><br>È possibile anche premere <strong>Ctrl</strong> e quindi <strong>fare doppio clic</strong> sul testo oppure <strong>cliccare e trascinare</strong> per creare selezioni multiple nel file. Modifica, taglia, copia o incolla allo stesso tempo.<br><br><strong>Dacci uno sguardo:</strong> È possibile inoltre creare velocemente diversi cursori alla fine di ciascuna riga, quindi <strong>Ctrl</strong> + <strong>cliccando</strong> oltre la fine delle righe.',
    ko: '<strong>Ctrl</strong> 키를 누른 후 편집하고자 하는 여러 곳을 <strong>클릭</strong>합니다. 입력을 시작합니다. <span class="appname"></span> 애플리케이션으로 파일의 모든 곳을 업데이트할 수 있습니다!<br><br>또한 <strong>Ctrl</strong> 키를 누른 후 텍스트를 <strong>더블클릭</strong>하거나 <strong>클릭한 후 드래그</strong>하여 파일에서 여러 곳을 선택할 수 있습니다. 한 번에 편집, 잘라내기, 복사 또는 붙여넣기할 수 있습니다.<br><br><strong>이 기능을 확인해 보십시오.</strong> 줄을 선택해서 줄 끝에 여러 캐럿들을 빠르게 생성한 후 줄 끝에서 <strong>Ctrl</strong>을 누른 상태에서 <strong>클릭</strong>합니다.',
    jp: '［<strong>Ctrl</strong>］ キーを押しながら、編集する各場所を<strong>クリック</strong>してください。入力を開始してください。<span class="appname"></span> では、すべての場所にあるファイルを一度に更新できます！<br><br>また、［<strong>Ctrl</strong>］ キーを押しながらテキストを<strong>ダブルクリック</strong>するか、<strong>クリックしてドラッグ</strong>することでもファイル内で複数の場所を同時に選択することができます。編集、切り取り、コピー、または貼り付けを一度にすべて行うことができます。<br><br><strong>注目機能：</strong>ラインの最後に複数のキャレットを作成するには、ラインを選択し、最後のラインの下まで ［<strong>Ctrl</strong>］ キーを押しながら<strong>クリック</strong>します。',
    ch: '按 <strong>Ctrl</strong> 键，然后<strong>单击</strong>您想要编辑的不同位置。开始键入。<span class="appname"></span> 可在所有位置更新您的文件！<br><br>您也可以按 <strong>Ctrl</strong> 键，然后<strong>双击</strong>文本或者<strong>单击并拖动</strong>以在文件中创建多选择。一次编辑、剪切、复制或粘贴全部。<br><br><strong>试试：</strong>您也可以通过选择行，迅速在行尾创建多个插入记号，然后按住 <strong>Ctrl</strong> 键并<strong>单击</strong>行尾以外区域。',
    cht: '請按一下<strong>Ctrl</strong>，然後在您想要編輯的不同地方<strong>按一下</strong>。開始打字。<span class="appname"></span>在所有位置更新您的檔案！<br><br>您也可以按一下<strong>Ctrl</strong>，然後在文字上<strong>按兩下</strong>或者<strong>按下並拖曳</strong>以在檔案中建立多個選擇。一次編輯、剪下、複製或貼上全部。<br><br><strong>試試：</strong>您也可以選取行，然後在使用<strong>Ctrl</strong> + <strong>點選</strong>以快速地在各行末建立多個脫字符號。'
  },

  f_ThemesHead: {
    en: 'Select your own layout and theme',
    de: 'Wählen Sie Layout und Thema nach Ihrem Geschmack',
    fr: '<span style="font-size: 85%;">Sélectionnez vos propres disposition et thème</span>',
    es: 'Seleccione su propio diseño y tema',
    it: 'Selezionare il proprio layout e tema',
    ko: '본인만의 레이아웃과 테마를 선택하십시오',
    jp: 'ご自身のレイアウトとテーマを選びます',
    ch: '选择您自己的布局和主题',
    cht: '選擇您自己的布局和主題'
  },

  f_Themes: {
    en: 'Want a clean look? Do you prefer multiple windows or no toolbars? Do you like a dark interface? Choose the layout and theme you prefer by clicking the Layout/Theme Selector on the main toolbar.',
    de: 'Wünschen Sie ein sauberes Aussehen?  Bevorzugen Sie mehrere Fenster oder keine Symbole? Gefällt Ihnen eine dunkle Oberfläche? Klicken Sie auf das Symbol Layout/Thema in der Hauptsymbolleiste und wählen Sie nach Ihrem Geschmack.',
    fr: 'Vous voulez un aspect net ? Vous préférez le multi-fenêtrage ? Vous ne voulez pas d\'icônes ? Vous voulez une interface sombre ? Sélectionnez la disposition et le thème que vous préférez en cliquant sur l\'icône du sélecteur de disposition/thème dans la barre d\'outils principale.',
    es: '¿Desea un aspecto despejado? ¿O prefiere varias ventanas o ningún icono? ¿Le gusta una interfaz oscura? Elija el diseño y el tema que más le guste haciendo clic en el icono del selector de diseños/temas en la barra de herramientas principal.',
    it: 'Cerchi uno stile pulito? Preferisci finestre multiple o non avere la barra strumenti? Desideri un\'interfaccia scura? Scegli il layout e il tema che preferisci cliccando sul Selettore Layout/Tema sulla barra degli strumenti principali.',
    ko: '말끔한 디자인을 원하십니까?  다중 창 또는 아이콘이 없는 상태를 선호하십니까? 어두운 인터페이스를 좋아하십니까? 메인 도구모음에서 레이아웃/테마 선택기 아이콘을 클릭해서 좋아하는 레이아웃과 테마를 선택하십시오.',
    jp: '清潔なルックスがいいですか？マルチウィンドウとアイコンなしのどちらをお好みですか？ 暗いインターフェースがお好きですか？お好みのレイアウトとテーマをメインツールバーにあるLayout（レイアウト）/Theme（テーマ）選択アイコンをクリックして選びます。',
    ch: '需要简洁的外观？您喜欢多窗口或不需要图标？ 您喜欢黑色界面吗？如果需要选择自己喜好的布局和主题，在主工具栏中点击布局/主题选择器图标即可。',
    cht: '需要簡潔的外觀？您喜歡多窗口或不需要圖標？ 您喜歡黑色界面嗎？如果需要選擇自己喜好的布局和主題，在主工具欄中點擊布局/主題選擇器圖標即可。'
  },

  f_CompareHead: {  // **** NEEDS TRANSLATION ****
    en: 'Quickly compare files and folders with <span class="bdlname"></span>',
    de: 'Schnelle Vergleiche von Dateien und Ordner mit <span class="bdlname"></span>',
    fr: 'Comparez rapidement fichiers et dossiers grâce à <span class="bdlname"></span>',
    es: 'Comparar rápidamente archivos y carpetas con <span class="bdlname"></span>',
    it: 'Confronta velocemente file e cartelle con <span class="bdlname"></span>',
    ko: '<span class="bdlname"></span>로 파일과 폴더를 신속하게 비교합',
    jp: '<span class="bdlname"></span>によりファイルやフォルダをすばやく比較',
    ch: '使用 <span class="bdlname"></span> 迅速比较文件和文件夹',
    cht: '使用 <span class="bdlname"></span> 快速比較檔案與資料夾'
  },

  f_Compare: {  // **** NEEDS TRANSLATION ****
    en: '<span class="appname"></span> includes <strong><span class="bdlabbr"></span></strong> for basic diff tasks such as comparing files and identifying differences. <br><br><strong>UC Pro</strong> provides a more complete feature set. <span class="bdlabbr"></span> Professional includes 2 and 3-way compare, folder compare and sync, merge, zip archive compare, ignore options, sessions, Word Doc / PDF compare, and more...',
    de: '<span class="appname"></span> enthält <strong><span class="bdlabbr"></span> Lite</strong> für Standard-Diff-Aufgaben wie Dateien vergleichen und Unterschiede identifizieren. <br><br> <strong><span class="bdlabbr"></span> Professional</strong> bietet einen umfangreicheren Funktionssatz. UC Pro enthält 2- und 3-fach Vergleiche, Ordnervergleiche und Synchronisation, Zusammenführung, Zip-Archiv-Vergleiche, Ignorieroptionen, Sitzungen, Word-Dokument- und PDF-Vergleiche und mehr...',
    fr: '<span style="font-size: 80%; line-height: 90%;"><span class="appname"></span> comprend une version <strong><span class="bdlabbr"></span> Lite</strong> destinée aux différentes tâches de différentiel, telles que la comparaison des fichiers et l\'identification des divergences. <br><br> <strong><span class="bdlabbr"></span> Professional</strong> dispose d\'un ensemble de fonctions plus complet. UC Pro comprend la comparaison bi ou trilatérale, la comparaison et synchronisation des dossiers, le fusionnement, les archive comprimées, des options d\'omission, des sessions, la comparaison des doc Word/PDF et plus encore...</span>',
    es: '<span style="font-size: 90%; line-height: 90%;"><span class="appname"></span> incluye <strong><span class="bdlabbr"></span> Lite</strong> para realizar tareas básicas de comparación de archivos e identificación de diferencias. <br><br> <strong><span class="bdlabbr"></span> Professional</strong> ofrece un conjunto de características más completo. UC Pro incluye opciones de comparación de dos y tres formas, comparación y sincronización de carpetas, combinación, comparación de archivos zip, posibilidad de ignorar opciones, sesiones, comparación de documentos Word/PDF y mucho más...</span>',
    it: '<span class="appname"></span> include <strong><span class="bdlabbr"></span> Lite</strong> per compiti base di differenziazione, come il confronto di file e l\'identificazione delle differenze. <br><br> <strong><span class="bdlabbr"></span> Professional</strong> fornisce un set più completo di caratteristiche. UC Pro include funzionalità di confronto a 2 e 3 vie, confronto e sincronizzazione cartelle, fusione, confronto archivi zip, opzioni ignora, sessioni, confronta doc Word / PDF e molto ancora...',
    ko: '<span class="appname"></span> 애플리케이션에는 파일을 비교 및 차이 식별 등의 기본적인 차이 식별 작업을 위한 <strong><span class="bdlabbr"></span> Lite</strong>가 포함되어 있습니다. <br><br> <strong><span class="bdlabbr"></span> Professional</strong>에는 더욱 완벽한 기능들이 제공됩니다. UC Pro 2방향 및 3방향 비교, 폴더 비교 및 동기화, 병합, zip 아카이브 비교, 무시 옵션, 세션, Word 문서 / PDF 비교 등의 기능이 포함되어 있습니다.',
    jp: '<span class="appname"></span>には、<strong><span class="bdlabbr"></span> Lite</strong>で利用できる、基本的な差分タスク向けのファイル比較機能や相違点の分析機能などが含まれます。<br><br> <strong><span class="bdlabbr"></span> Professional</strong> では、より充実した機能をご利用いただけます。UC Pro では、2ウェイ・3ウェイの検索、フォルダの比較と同期、結合、zipアーカイブの比較、オプションの無視、セッション、Word ドキュメント/ PDFの比較などの多くの機能がご利用頂けます。',
    ch: '<span class="appname"></span> 包括 <strong><span class="bdlabbr"></span> Lite</strong>，可进行基本的区分任务，如比较文件和识别差异。<br><br> <strong><span class="bdlabbr"></span> Professional</strong> 提供更完整的功能组合。<span class="bdlabbr"></span> Professional 包括双方和三方比较、文件夹比较与同步、合并、压缩存档文件比较、忽略选项、会话、Word 文件/ PDF 比较，等等...',
    cht: '<span class="appname"></span>包含<strong><span class="bdlabbr"></span> Lite</strong>可進行基本的區分任務，例如，比較檔案與識別其差異。<br><br> <strong><span class="bdlabbr"></span> Professional</strong> 則提供較完整的功能組合。UC Pro 包含 2 與 3 向比較、資料夾比較與同步、合併、壓縮歸檔比較、忽略選項、工作階段、Word 文件/PDF 比較與更多功能...'
  },

  f_findReplaceHead: {  // **** NEEDS TRANSLATION ****
    en: 'Find/replace across a single file, multiple files, and more',
    de: 'Suchen/Ersetzen in einer Datei, mehreren Dateien und mehr',
    fr: 'Rechercher/Remplacer dans un seul fichier, plusieurs fichiers, et plus',
    es: 'Buscar y reemplazar en un único archivo, varios archivos y mucho más',
    it: '<span style="font-size: 90%;">Trova/sostituisci in un singolo file, più file e altro</span>',
    ko: '단일 파일, 복수 파일 찾기/바꾸기 등',
    jp: '<span style="font-size: 90%;">単一ファイルや複数ファイルの検索や置換など多機能</span>',
    ch: '在单个文件、多个文件中查找/替换',
    cht: '在單一檔案、多個檔案間進行尋找與取代與更多功能'
  },

  f_findReplace: {  // **** NEEDS TRANSLATION ****
    en: 'Press Ctrl + F to quickly search for text with Quick Find, or press Ctrl + F a second time to open the main Find dialog.  Ctrl + R opens the Replace dialog.  All find and replace options are available in the Search menu. <br><br>Advanced find / replace options include regular expressions, find and replace in files, search in column, and much more...',
    de: 'Drücken Sie Strg + F, um mit Quick Find schnell nach Text zu suchen oder drücken Sie ein zweites Mal Strg + F, um das Hauptdialogfeld für die Suche zu öffnen.  Strg + R öffnet das Dialogfeld Ersetzen.  Alle Such- und Ersatzoptionen stehen im Suchmenü zur Verfügung. <br><br> Erweiterte Such-/Ersatzoptionen umfassen reguläre Ausdrücke, Suchen und Ersetzen in Dateien, Suche in Spalten und vieles mehr...',
    fr: '<span style="font-size: 90%;">Appuyez sur Ctrl + F pour rechercher rapidement du texte avec Quick Find et sur Ctrl + F une deuxième fois pour ouvrir la boîte de dialogue de la recherche.  Ctrl + R permet d\'ouvrir la boîte de dialogue du remplacement.  Toutes les options de recherche et de remplacement sont disponibles dans le menu Recherche. <br><br> Les options de recherche /remplacement avancées comprennent les expressions régulières, la recherche et le remplacement dans les fichiers, la recherche dans les colonnes, et plus encore...</span>',
    es: '<span style="font-size: 90%;">Pulse Ctrl + F para buscar rápidamente texto con la búsqueda rápida o presione Ctrl + F otra vez para abrir el cuadro de diálogo principal Buscar.  Ctrl + R abre el cuadro de diálogo Reemplazar.  Todas las opciones de buscar y reemplazar están disponibles en el menú Buscar. <br><br> Las opciones avanzadas de búsqueda y sustitución incluyen expresiones regulares, buscar y reemplazar en archivos, buscar en columnas, y mucho más...</span>',
    it: '<span style="font-size: 90%;">Premere Ctrl + F per la ricerca veloce di testo con la funzione Ricerca veloce oppure premere Ctrl + F una seconda volta per aprire la finestra di dialogo principale Trova.  La combinazione Ctrl + R apre la finestra di dialogo Sostituisci.  Tutte le opzione di ricerca e sostituzione sono disponibili nel menu Ricerca. <br><br> Le opzioni avanzate di ricerca / sostituzione includono espressioni regolari, trova e sostituisci nei file, cerca nella colonna e molto ancora.</span>',
    ko: 'Ctrl + F를 눌러서 빠른 검색 기능으로 텍스트를 신속하게 검색, 또는 Ctrl + F를 또 한 번 눌러서 기본 검색 대화상자를 엽니다.  Ctrl + R을 눌러서 바꾸기 대화상자를 엽니다.  검색 메뉴에서 모든 찾기 및 바꾸기 옵션을 사용할 수 있습니다. <br><br> 고급 찾기 / 바꾸기 옵션에는 정규 표현식,  파일 내에서 찾기 및 바꾸기, 열에서 검색 등의 기능이 포함됩니다.',
    jp: '<span style="font-size: 95%;">テキストのクイック検索を開くには、［Ctrl］キーと ［F］ を同時に押します。メインの検索ダイアログを開くには、もう一度 ［Ctrl］キーと ［F］ を同時に押します。［Ctrl］キーと ［R］ を同時に押すと、置換ダイアログが開きます。検索と置換のオプションはすべて [検索] メニューから利用できます。<br><br>高度な検索/置換オプションでは、正規表現の利用、ファイル内の検索・置換、列内の検索などの多くの機能が利用できます。</span>',
    ch: '按 Ctrl + F 键以使用“快速查找”迅速搜索文本，或者再次按 Ctrl + F 键以打开主“查找”对话框。按 Ctrl + R 键会打开“替换”对话框。可在“搜索”菜单中找到所有查找和替换选项。<br><br>高级查找/替换选项包括正则表达式、在文件中查找和替换、在列中搜索等...',
    cht: '按一下 Ctrl + F 以使用 Quick Find 進行快速文字搜尋，或者再次按下 Ctrl + F 可開啟主要「尋找」對話方塊。按下 Ctrl + R 可開啟「取代」對話方塊。可在「搜尋」選單內找到所有尋找與取代選項。<br><br> 進階尋找/取代選項包含一般表達式、在檔案內的尋找與取代、在欄位中的搜尋以及更多功能...'
  },

  f_FTPHead: {  // **** NEEDS TRANSLATION ****
    en: 'Edit files from FTP / SFTP servers',
    de: 'Dateien von FTP-/SFTP-Servern bearbeiten',
    fr: 'Modifier les fichiers depuis les serveurs FTP / SFTP',
    es: '<span style="font-size: 90%;">Editar archivos desde servidores FTP/SFTP</span>',
    it: 'Modifica file da server FTP / SFTP',
    ko: 'FTP / SFTP 서버 파일 편집 ',
    jp: 'FTP / SFTPサーバからファイルを編集する',
    ch: '从 FTP / SFTP 服务器编辑文件',
    cht: '從 FTP/SFTP 伺服器編輯檔案'
  },

  f_FTP: {  // **** NEEDS TRANSLATION ****
    en: 'Whether you&#39;re using <span class="appname"></span> for development or basic editing, you may need FTP. Set up your FTP account (via the <strong>File</strong> menu) and take advantage of the built-in FTP open and save options as well as the FTP browser. <br><br>Need to compare changed files or folders on your server? No problem - use <span class="bdlabbr"></span> Pro&#39;s built in FTP compare. You can even sync up your local / remote directories.',
    de: 'Unabhängig davon, ob Sie <spa class="appname"></span> zur Entwicklung oder zur Standard-Bearbeitung verwenden, Sie werden vermutlich FTP brauchen. Richten Sie Ihr FTP-Konto ein (über das Menü <strong>Datei</strong>) und nutzen Sie die integrierten FTP-Öffnungs- und Speicheroptionen sowie den FTP-Browser.',  // <br><br> Müssen Sie vergleichen, welche Dateien oder Ordner auf Ihrem Server geändert wurden? Kein Problem – verwenden Sie <span class="bdlabbr"></span> Pros integriertes FTP-Vergleichsprogramm. Sie können sogar Ihre lokalen und externen Verzeichnisse synchronisieren.
    fr: 'Que vous vous serviez de <span class="appname"></span> pour le développement ou l\'édition de base, il se peut que vous ayez besoin de FTP. Configurez votre compte FTP (dans le menu <strong>Fichier</strong>) et tirez parti du FTP et des options d\'enregistrement intégrées ainsi que du navigateur FTP.', //  <br><br> Besoin de vérifier quels fichiers ou dossiers ont fait l\'objet de modifications sur votre serveur ? Pas de problème - servez-vous de la comparaison FTP intégrée de <span class="bdlabbr"></span> Pro. Vous pouvez même synchroniser vos répertoires locaux et distants.
    es: '<span style="font-size: 90%;">Aunque utilice <span class="appname"></span> para el desarrollo o para la edición básica, es posible que necesite una FTP. Configure su cuenta FTP (mediante el menú <strong>Archivo</strong>) y aprovéchese de las opciones de abrir y guardar de la FTP integrada así como del navegador FTP. <br><br> ¿Tiene que averiguar qué archivos o carpetas han cambiado en su servidor? No hay problema - utilice la comparación de FTP integrada de <span class="bdlabbr"></span> Pro. Incluso puede sincronizar sus directorios locales y remotos.</span>',
    it: '<span style="font-size: 90%;">Che tu stia utilizzando <span class="appname"></span> per lo sviluppo o l\'editing base, è possibile che tu abbia bisogno di un FTP. Configura il tuo account FTP ( (attraverso il menu <strong>File</strong> ) e sfrutta al meglio il vantaggio delle opzioni integrate apri e salva FTP, così come del browser FTP. <br><br> Hai bisogno di confrontare quali file o cartelle sono state modificate sul tuo server? Nessun problema - usa la funzione integrata di confronto FTP di <span class="bdlabbr"></span> Pro. È possibile persino sincronizzare le proprie directory locali e remote.</span>',
    ko: '개발이나 기본 편집 작업에 <span class="appname"></span> 애플리케이션을 이용하든 안 하든, FTP가 필요할 수 있습니다. FTP 계정을 (<strong>파일</strong> 메뉴를 이용해서) 설정한 후 내장 FTP 열기 및 저장 옵션과 FTP 브라우저의 기능을 이용하십시오. <br><br> 서버에서 어떤 파일과 폴더가 변경되었는 지 비교할 필요가 있습니까? 걱정하지 마십시오 - <span class="bdlabbr"></span> Pro에 내장된 FTP 비교 기능을 이용하십시오. 로컬 디렉토리와 원격 디렉토리도 동기화할 수 있습니다.',
    jp: '<span class="appname"></span>を開発に使用するか、あるいは基本的な編集に用いるかに関わらず、FTPが必要となる場合があります。FTPアカウント（<strong>ファイル</strong>メニューを経由）を設定し、FTPファイルを開く・保存する際のオプションやFTPブラウザなどの機能をご活用ください。<br><br>サーバー上で変更されたファイルやフォルダの内容を比較する必要がありますか？<span class="bdlabbr"></span> Proに内蔵されたFTP比較機能が役に立ちます。また、ローカルディレクトリやリモートディレクトリを同期することもできます。',
    ch: '无论您是否使用 <span class="appname"></span> 进行开发或基本编辑，您都可能需要 FTP。设置您的 FTP 帐户（通过<strong>文件</strong>菜单），并利用内置 FTP 打开和保存选项，以及 FTP 浏览器。<br><br>需要比较服务器上哪些文件或文件夹已更改？没问题 - 使用 <span class="bdlabbr"></span> Pro&#39;s 内置 FTP 比较。您甚至可以同步您的本地和远程目录。',
    cht: '無論您是否&#39;使用<span class="appname"></span>進行開發或基本編輯，您都可能需要 FTP。請設定您的 FTP 帳戶（透過<strong>檔案</strong>選單），並善用內建 FTP 的優勢來開啟與儲存選項，以及 FTP 瀏覽器。<br><br>需要比較您伺服器上已經變更了哪些檔案或資料夾嗎？沒問題 — 請使用 FTP 比較中內建的 <span class="bdlabbr"></span> Pro&#39;。您甚至可以同步處理您的本機與遠端目錄。'
  },

  f_ColumnHead: {  // **** NEEDS TRANSLATION ****
    en: 'Column mode and block select',
    de: 'Spaltenmodus und Blockauswahl',
    fr: 'Mode colonne et sélection de blocs',
    es: 'Modo de columna y selección de bloques',
    it: 'Modalità colonna e selezione blocchi',
    ko: '열 모드 및 블록 선택',
    jp: '列モードとブロック選択',
    ch: '列模式和块选择',
    cht: '欄位模式與區塊選擇'
  },

  f_Column: {  // **** NEEDS TRANSLATION ****
    en: 'Column mode allows you to select columns and rows of text as opposed to only selecting rows. <br><br>To use column mode, press <strong>Alt</strong> + <strong>C</strong>.  You can also hold down the <strong>Alt</strong> key and <strong>click and drag</strong> to make quick block selections. Type, cut, copy, paste and more - all in column mode!',
    de: 'Im Spaltenmodus können Sie Spalten und Textzeilen, und nicht nur Zeilen, markieren. <br><br> Drücken Sie zur Verwendung des Spaltenmodus <strong>Alt</strong> + <strong>C</strong>.  Sie können die <strong>Alt</strong>-Taste auch gedrückt halten und Spalten für die schnelle Blockauswahl <strong>anklicken und ziehen</strong>. Tippen, ausschneiden, einfügen und mehr – alles im Spaltenmodus!',
    fr: 'Le mode colonne vous permet de sélectionner et des colonnes et des rangées de texte. <br><br> Pour activer le mode colonne, appuyez sur <strong>Alt</strong> + <strong>C</strong>.  Vous pouvez également maintenir appuyée la touche <strong>Alt</strong> et effectuez un <strong>cliquer/glisser</strong> pour rapidement sélectionner des blocs. Tapez, coupez, copiez, collez , et plus encore - tout cela en mode colonne !',
    es: 'El modo de columna le permite seleccionar columnas y filas de texto, frente a elegir solo filas. <br><br> Para utilizar el modo de columna, presione <strong>Alt</strong> + <strong>C</strong>.  También puede mantener presionada la tecla <strong>Alt</strong> y <strong>hacer clic y arrastrar</strong> para hacer rápidas selecciones de bloques. Escriba, corte, copie, pegue y mucho más... todo en modo de columna.',
    it: 'La modalità colonna consente di selezionare colonne e righe di un testo in perfetta opposizione solo con le righe di selezione. <br><br> Per utilizzare la modalità colonna, premere <strong>Alt</strong> + <strong>C</strong>.  È inoltre possibile tenere premuto il tasto <strong>Alt</strong> e, <strong>cliccando, trascinare</strong> per selezionare rapidamente interi blocchi. Digita, taglia, copia, incolla e molto ancora - tutto in modalità colonna!',
    ko: '열 모드에서는 행만 선택할 수 있는 경우와 대조적으로 텍스트의 행과 열을 선택할 수 있습니다. <br><br> 열 모드를 이용하려면 <strong>Alt</strong> + <strong>C</strong>를 누릅니다.  <strong>Alt</strong> 키를 누른 상태에서 <strong>클릭 및 드래그</strong>하여 신속하게 블록을 선택할 수 있습니다. 열 모드에서 입력, 잘라내기, 복사, 붙여넣기 등의 작업을 수행할 수 있습니다!',
    jp: '列モードでは、行のみを選択する場合と異なり、テキストの列と行を選択することができます。<br><br>列モードを使用するには、［<strong>Alt</strong>］ キーと ［<strong>C</strong>］を同時に押します。また、［<strong>Alt</strong>］ キーを押しながら<strong>クリックしてドラッグ</strong>することにより、素早くブロックを選択することができます。入力、切り取り、コピー、ペーストはすべて列モードから！',
    ch: '列模式允许您选择文本的列和行，而非只是选择行。<br><br>若要使用列模式，请按 <strong>Alt</strong> + <strong>C</strong> 键。您也可以按住 <strong>Alt</strong> 键，然后<strong>单击并拖动</strong>以迅速选择块。在列模式下完成键入、剪切、复制、粘贴等！',
    cht: '欄位模式可讓您選取文字的欄位與列，而非僅是選擇列。<br><br> 若要使用欄位模式，請按下<strong>Alt</strong> + <strong>C</strong>。您也可以按下<strong>Alt</strong>鍵並<strong>按下拖曳</strong>以快速選取區塊。在欄位模式下完成輸入、剪下、貼上與更多功能！'
  },

  f_TemplatesHead: {  // **** NEEDS TRANSLATION ****
    en: 'Smart templates / code snippets',
    de: 'Smart-Vorlagen/Codeausschnitte',
    fr: 'Modèles intelligents / bouts de code',
    es: '<span style="font-size: 85%;">Plantillas inteligentes/fragmentos de código</span>',
    it: '<span style="font-size: 85%;">Template intelligenti / frammenti codici</span>',
    ko: '스마트 템플릿 / 코드 정보',
    jp: 'スマートテンプレート/コードスニペット',
    ch: '智能模板/代码片段',
    cht: '智慧型範本/代碼片段'
  },

  f_Templates: {  // **** NEEDS TRANSLATION ****
    en: 'With smart templates, you quickly insert code snippets (either automatically when you type a keyword or via auto-complete) based upon the type of file you&#39;re editing. Also, you can add custom variables to your templates. <br><br>There are several pre-configured smart templates for most source code languages, and you can also create or modify your own in the <strong>Advanced</strong> menu.',
    de: '<span style="font-size: 90%;">Mit Smart-Vorlagen können Sie schnell Codeausschnitte einfügen (entweder automatisch beim Tippen eines Schlüsselwortes oder durch Auto-Vervollständigen), je nach zu bearbeitender Datei. Sie können Ihrer Vorlage auch individuelle Variablen hinzufügen. <br><br> Es gibt mehrere vorkonfigurierte Smart-Vorlagen für die meisten Quellcodesprachen, und Sie können im Menü <strong>Erweitert</strong> auch Ihre eigenen erstellen oder ändern.</span>',
    fr: '<span style="font-size: 90%; line-height: 90%;">Grâce aux modèles intelligents, vous pouvez insérer rapidement des bouts de code (automatiquement à la saisie d\'un mot ou au moyen de l\'autoremplissage) en fonction du type de fichier que vous modifiez. Par ailleurs, vous pouvez ajouter des variables personnalisées à vos modèles. <br><br> Plusieurs modèles intelligents préconfigurés adaptés à la plupart des langages de programmation sources sont disponibles, et vous pouvez créer ou modifier le vôtre dans le menu <strong>Avancé</strong>.</span>',
    es: '<span style="font-size: 90%;">Con las plantillas inteligentes, puede insertar rápidamente fragmentos de código (ya sea automáticamente cuando escriba una palabra o mediante la función de autocompletar) en función del tipo de archivo que está editando. Además, puede añadir variables personalizadas a sus plantillas. <br><br> Hay varias plantillas inteligentes preconfiguradas para la mayoría de los lenguajes de código fuente y también puede crear o modificar las suyas propias en el menú <strong>Avanzado</strong>.</span>',
    it: '<span style="font-size: 90%;">Con i template intelligenti, è sufficiente inserire frammenti di codici (sia automaticamente quando si digita una parola chiave oppure con completamento automatico) basati sul tipo di file che si sta modificando. Inoltre, è possibile aggiungere variabili personalizzate ai template. <br><br> Esistono diversi template intelligenti preconfigurati per la maggior parte dei linguaggi dei codici sorgente ed è possibile crearli o modificarli da soli nel menu <strong>Avanzate</strong>.</span>',
    ko: '스마트 템플릿 기능을 이용해서 편집 중인 파일의 유형에 근거하여 코드 정보를 신속하게 (키워드 입력 시 자동으로 또는 자동 완성 기능으로) 삽입할 수 있습니다. 또한 템플릿에 사용자 지정 변수를 추가할 수도 있습니다. <br><br> 대부분의 소소 코드 언어를 위한 사전 구성된 여러 개의 스마트 템플릿이 있으며, <strong>고급</strong> 메뉴에서 사용자만의 템플릿을 작성하거나 수정할 수도 있습니다.',
    jp: 'スマートテンプレートを使用すると、編集中のファイルの種類に基づいてコードスニペットを（キーワードを入力した際に自動的に、またはオートコンプリートにより）素早く挿入することができます。また、テンプレートにカスタム変数を追加することもできます。<br><br>予め設定されたスマートテンプレートがほとんどのソースコード言語ごとにいくつか用意されており、<strong>高度な</strong>メニューから独自に作成したり、変更したりすることもできます。',
    ch: '使用智能模板，您可以基于您正在编辑的文件的类型，迅速插入代码片段（在您键入关键词时自动插入或者通过自动完成）。而且，您可以向模板中添加自定义变量。<br><br>大多数源代码语言都有几种预配置智能模板，您也可以在<strong>高级</strong>菜单中创建或修改自己的模板。',
    cht: '使用智慧型範本，您可根據您&#39;所編輯的檔案類型而定，可以快速插入代碼片段（可以在您輸入關鍵字時自動插入，或者透過自動填寫功能）。另外，您也可以新增自訂變數到您的範本。<br><br>針對大多數來源程式碼語言，有提供數種預先組態的智慧型範本，且您也可以在<strong>進階</strong>選單中建立或修改您自己的範本。'
  },

  f_SortHead: {  // **** NEEDS TRANSLATION ****
    en: 'Sort selected text or the entire file',
    de: 'Markierten Text oder die gesamte Datei sortieren',
    fr: '<span style="font-size: 70%;">Trier le texte sélectionné ou le fichier dans son ensemble</span>',
    es: '<span style="font-size: 90%;">Ordenar texto seleccionado o todo el archivo</span>',
    it: 'Ordina il testo selezionato o l\'intero file',
    ko: '선택된 텍스트 또는 전체 파일 정렬',
    jp: '選択したテキストまたはファイル全体の並び替え',
    ch: '对选定的文本或整个文件进行排序',
    cht: '排序所選取的文字或整個檔案'
  },

  f_Sort: {  // **** NEEDS TRANSLATION ****
    en: 'Quickly sort a file or selection alphabetially or numerically, ascending or descending. Sort based upon one or more column ranges and optionally remove duplicate entries.  Sort with basic or advanced options. <br><br>Sorting can help you read log files, sort fields in flat file databases, or organize any text. You can access the sort feature in the <strong>File</strong> menu.',
    de: 'Sie können eine Datei oder Auswahl alphabetisch oder numerisch, auf- oder absteigend schnell sortieren. Sortieren Sie einen oder mehrere Spaltenbereiche und entfernen Sie optional duplizierte Einträge.  Sortieren Sie mit Standard- oder erweiterten Optionen. <br><br> Sortieren vereinfacht das Lesen von Log-Dateien, ordnet Felder in  Flatfile-Datenbanken oder organisiert jeden Text. Sie können auf die Sortierfunktion über das Menü <strong>Datei</strong> zugreifen.',
    fr: '<span style="font-size: 85%;">Triez rapidement un fichier ou une sélection alphabétiquement ou numériquement, par ordre croissant ou décroissant. Triez en fonction d\'une ou plusieurs plages de colonnes, et supprimez les entrées en double.  Triez au moyen d\'options basiques ou avancées. <br><br> Le tri peut vous aider à lire les fichiers journaux, trier les champs dans les bases de données à fichiers plats ou organiser des textes. La fonction Tri est accessible depuis le menu <strong>Fichier</strong>.</span>',
    es: 'Ordene rápidamente un archivo o una selección en orden alfabético o numérico, en orden ascendente o descendente. Ordene en función de uno o varios rangos de columna y suprima entradas duplicadas, si lo desea.  Ordene con opciones básicas o avanzadas. <br><br> La ordenación puede ayudarle a leer archivos de registro, ordenar campos en bases de datos de archivos sin formato o a organizar cualquier texto. Puede acceder a la función de ordenación en el menú <strong>Archivo</strong>.',
    it: '<span style="font-size: 90%;">Per ordinare velocemente un file o una selezione alfabeticamente o numericamente, in ordine crescente o decrescente. Per ordinare sulla base di uno o più intervalli di colonna e rimuovere opzionalmente inserimenti duplici.  Per ordinare con opzioni basi o avanzate. <br><br> L\'ordinamento può aiutarti a leggere i file di registro, a ordinare i campi in database di file oppure a organizzare qualsiasi testo. È possibile accedere alla funzione Ordina nel menu <strong>File</strong>.</span>',
    ko: '파일 또는 선택 영역을 알파벳 순 또는 숫자 순으로 오름차순 또는 내림차순으로 신속하게 정렬합니다. 한 개 이상의 열 범위에 기초하여 정렬하며 중복된 항목을 삭제할 수도 있습니다.  기본 옵션 또는 고급 옵션으로 정렬합니다. <br><br> 정렬 기능을 이용해서 로그 파일 읽기, 플랫 파일 데이터베이스의 필드 정렬,  모든 텍스트 정리 작업을 수행할 수 있습니다. 정렬 기능은 <strong>파일</strong> 메뉴에서 이용할 수 있습니다.',
    jp: '<span style="font-size: 90%;">単一または選択されたファイルを、アルファベット順や数字順（昇順・降順）に素早く並び替えることができます。1つ以上の列の範囲を基に並び替えを行い、必要に応じて重複したエントリを削除します。基本的または高度な並び替えオプションが利用できます。<br><br>並び替え機能は、ログファイルの解読、フラットファイルデータベース内の項目の並べ替え、テキストの整理などに役立ちます。並び替え機能は、［<strong>ファイル</strong>] メニューからアクセスすることができます。</span>',
    ch: '按字母或数字顺序对文件或选择迅速进行升序或降序排序。基于一列或多列进行排序，并通过选择删除重复条目。使用基本选项或高级选项进行排序。<br><br>排序可帮助您阅读日志文件，在平面文件数据库中排序字段，或者组织任何文本。您可以在<strong>文件</strong>菜单中访问排序功能。',
    cht: '使用字母或數字順序，以升冪或降冪方式快速排序檔案或選取的檔案。根據一或多個欄位範圍進行排序，並選擇性地移除重複的項目。使用基本或進階選項排序。<br><br>排序可協助您讀取記錄檔案，在純資料檔案資料庫中排序欄位，或組織任何文字。您可以在<strong>檔案</strong>選單中存取排序功能。'
  },

  f_WordfileHead: {
    en: 'Add syntax highlighting for other languages by adding wordfiles',
    de: 'Syntax-Hervorhebung f&uuml;r weitere Sprachen durch Wortlistendateien',
    fr: 'Ajoutez la coloration syntaxique pour d\'autres langages en ajoutant des &laquo;&nbsp;wordfiles&nbsp;&raquo;',
    es: 'Añada sintaxis de otros lenguajes en el archivo de palabras',
    it: 'Aggiungi l\'evidenziazione della sintassi per altri linguaggi aggiungendo wordfile',
    ko: '워드파일을 추가하여 구문강조에 다른 언어를 추가하기',
    jp: 'ワードファイルを追加して他の言語のシンタックスハイライトを追加してください',
    ch: '用赠加一个字文件来增加其它语言的语法突出',
    cht: '透過加入 wordfile 來為其他語言加入語法突顯'
  },

  f_Wordfile: {  // **** NEEDS TRANSLATION ****
    en: '<span class="appname"></span> natively supports syntax highlighting for 14 commonly-used coding languages.  However, you can add a new language by simply downloading and saving a new wordfile into the correct location. Over 600 extra wordfiles are available for download from our site!',
    de: '<span class="appname"></span> unterstützt Syntaxmarkierungen für 14 bevorzugt verwendete Programmiersprachen.  Sie können jedoch eine neue Sprache hinzufügen, indem Sie einfach ein neues Wordfile an den korrekten Speicherplatz herunterladen und speichern. Auf unserer Website werden über 600 zusätzliche Wordfiles zum Herunterladen angeboten!',
    fr: '<span class="appname"></span> prend en charge la surbrillance de syntaxe de manière native pour 14 des langages de programmation les plus courants.  Toutefois, vous pouvez ajouter un nouveau langage en téléchargeant et enregistrant un nouveau fichier word dans l\'emplacement correspondant. Plus de 600 fichiers word sont disponibles en téléchargement sur notre site !',
    es: '<span class="appname"></span> admite de manera nativa el resaltado de sintaxis para los 14 lenguajes de codificación más utilizados.  Sin embargo, puede agregar un nuevo lenguaje simplemente descargando y guardando un nuevo archivo de palabras en la ubicación correcta. En nuestro sitio web puede descargar más de 600 archivos de palabras adicionales.',
    it: '<span class="appname"></span> supporta nativamente l\'evidenziazione della sintassi per 14 linguaggi di codifica tra quelli più comunemente usati.  Tuttavia, si può aggiungere un nuovo linguaggio semplicemente scaricando e salvando un nuovo wordfile nella giusta posizione. Oltre 600 extra wordfile sono disponibili per il download dal nostro sito!',
    ko: '<span class="appname"></span> 애플리케이션은 일반적으로 사용되는 14개 코딩 언어의 구문 강조 기능을 지원합니다.  새 언어의 경우에는 새 워드파일을 올바른 위치에 다운로드한 후 저장하기만 하면 새 언어를 추가할 수 있습니다. 당사 사이트에서 600여개의 추가적인 워드파일들을 다운로드할 수 있습니다!',
    jp: '<span class="appname"></span>では、一般的に使用される14のコーディング言語による構文強調表示にネイティブで対応しています。また、新しく言語を追加する作業は、新しいワードファイルをダウンロードし指定場所に保存するだけで簡単に完了します。当社のWebサイトからは、600以上のワ​​ードファイルをダウンロードして追加できます！',
    ch: '<span class="appname"></span> 自身针对 14 种常用编码语言支持语法突显功能。但是，您只需将新的词语文件下载并保存到正确位置，即可添加一种新语言。可从我们的网站下载超过 600 种额外的词语文件！',
    cht: '<span class="appname"></span>原生支援對 14 種最常使用的編碼語言的語法標示功能。然而，您可以下載並儲存新的 WORD 檔案到正確的位置來新增新語言。可從我們的網站下載超過 600 種額外的 WORD 檔案！'
  },

  f_FindReplaceFilesHead: {
    en: 'Find and Replace in Files',
    de: 'Suchen / Ersetzen in Dateien',
    fr: 'Rechercher / Remplacer dans des fichiers',
    es: 'Buscar y Cambiar archivos',
    it: 'Trova / Sostituisci nei File',
    ko: '파일 검색 및 교체',
    jp: 'ファイルの検索/置換',
    ch: '文件的寻找/替换',
    cht: '多重檔案搜尋/取代'
  },

  f_FindReplaceFiles: {  // **** NEEDS TRANSLATION ****
    en: 'The Find in Files/Replace in Files options are accessible under the <strong>Search</strong> menu. These powerful search options allow you to search for strings or text within multiple files contained in a directory.<br><br>With Find/Replace in Files, you have all of the features available to you with Find/Replace plus the option to search subdirectories, Project/Favorite Files, files by type, and more, with advanced ignore options.',
    de: 'Der Zugriff auf die Optionen In Dateien suchen/In Dateien ersetzen erfolgt über das Menü <strong>Suchen</strong>. Mit diesen leistungsstarken Suchoptionen können Sie nach Zeichenketten oder Text in mehreren Dateien eines Verzeichnisses suchen.<br><br>Mit In Dateien suchen/In Dateien ersetzen verfügen Sie über alle Funktionen von Suchen/Ersetzen, haben aber zusätzlich die Option, Unterverzeichnisse, Projekte/Dateien in Favoriten, Dateien nach Typ und mehr mit erweiterten Ignorieroptionen zu suchen.',
    fr: 'Les options Rechercher/Remplacer dans les fichiers sont accessibles dans le menu <strong>Rechercher</strong>. Ces options de recherche puissantes permettent de rechercher des chaînes ou du texte dans plusieurs fichiers contenus dans un répertoire.<br><br>Au moyen de Rechercher/Remplacer dans les fichiers, outre les options ordinaires de Rechercher/Remplacer, vous pourrez effectuer des recherches dans les répertoires, les fichiers de projet/favoris, les fichiers par type, et plus encore, grâce aux options d\'omission avancées.',
    es: 'Las opciones Buscar en archivos/Reemplazar en archivos están accesibles en el menú <strong>Buscar</strong>. Estas potentes opciones de búsqueda le permiten buscar cadenas o texto en varios archivos contenidos en un directorio.<br><br>Con Buscar/Reemplazar en archivos tendrá todas las funciones disponibles con Buscar/Reemplazar más la opción de buscar en subdirectorios, buscar archivos de proyecto y favoritos, archivos por tipo, y mucho más, con opciones avanzadas de omisión.',
    it: 'Le opzioni Trova nei File/Sostituisci nei file sono accessibili nel menu <strong>Ricerca</strong> menu. Queste potenti opzioni di ricerca consentono di ricercare stringhe o testi all\'interno di diversi file contenuti in una directory.<br><br>Con la funzione Trova/Sostituisci nei file, si dispone di tutte le funzionalità disponibili con la funzione Trova/Sostituisci, più l\'opzione di ricerca nelle sotto-directory, nei file Progetto/Preferiti, nei file cercando per tipo e molto ancora con opzioni avanzate della funzione Ignora.',
    ko: '파일에서 검색/파일에서 바꾸기 옵션은 <strong>검색</strong> 메뉴에서 이용할 수 있습니다. 이들 강력한 검색 옵션을 이용해서 디렉토리에 포함된 여러 파일의 문자열 또는 텍스트를 검색할 수 있습니다.<br><br>파일에서 검색/바꾸기 기능을 이용해서 검색/바꾸기와 더불어 하위 디렉토리 검색, 프로젝트/즐겨찾는 파일, 유형별 파일 등의 옵션과 고급 무시 옵션 등의 모든 기능을 이용할 수 있습니다.',
    jp: '［ファイル内を検索］ や ［ファイル内を置換］ の各オプションは、<strong>検索</strong>メニューからアクセスできます。これらの強力な検索オプションを使用すると、ディレクトリに含まれる複数のファイル内の文字列やテキストを検索することができます。<br><br>［ファイル内を検索］ や ［ファイル内を置換］では、通常の検索/置換で利用できる機能に加え、サブディレクトリやプロジェクト/お気に入りのファイルの検索、ファイルの種類別検索や高度な無視オプションなどが利用できます。',
    ch: '在<strong>搜索</strong>菜单中，可找到“在文件中查找/在文件中替换”选项。这些功能强大的搜索选项可让您在一个目录的多个文件中搜索字符串或文本。<br><br>使用“在文件中查找/替换”选项，您可获得“查找/替换”提供的所有功能，以及搜索子目录选项、项目/收藏文件选项、按类型等归档选项、以及高级忽略选项。',
    cht: '可從<strong>搜尋</strong>選單中的「檔案」選項存取「在檔案中尋找/在檔案中取代」選項。這些功能強大的搜尋選項可讓您搜尋目錄中所包含的多個檔案內的字串或文字。<br><br>使用「在檔案內尋找/取代」功能，您可獲得「尋找/取代」的完整功能，以及可搜尋子目錄、專案/最愛資料夾、依檔案類型的選項與更多功能，以及更進階的忽略選項。'
  },

  f_FunctionListHead: {  // **** NEEDS TRANSLATION ****
    en: 'Quickly jump to function definitions with the Function List',
    de: 'Springen Sie mit der Funktionsliste schnell zu Funktionsdefinitionen',
    fr: 'Accéder rapidement aux définitions avec la liste des fonctions',
    es: 'Salte rápidamente a las definiciones de función con la lista de funciones.',
    it: 'Per passare velocemente alle definizioni delle funzioni con l\'Elenco Funzioni',
    ko: '함수 목록을 이용해서 신속하게 함수 정의로 이동',
    jp: '関数リストにより、関数定義に素早くジャンプ',
    ch: '使用“功能”列表迅速跳到功能定义',
    cht: '使用「功能清單」，快速跳至功能定義'
  },

  f_FunctionList: {  // **** NEEDS TRANSLATION ****
    en: 'The Function List displays all functions in the active file (or project). You can double-click a function name in this list to jump to its definition in your source file.<br><br>If you don&#39;t see the Function List, you can quickly enable it by pressing <strong>F8</strong>.  You can right-click on the Function List to access its options.',
    de: 'Die Funktionsliste zeigt alle Funktionen der aktiven Datei (oder des Projektes) an. Sie können auf einen Funktionsnamen in dieser Liste doppelklicken, um zu seiner Definition in Ihrer Quelldatei zu springen.<br><br> Wenn Sie die Funktionsliste nicht sehen, können Sie sie durch Drücken der <strong>F8</strong>-Taste schnell aktivieren.  Wenn Sie mit der rechten Maustaste auf die Funktionsliste klicken, werden Ihnen die Optionen angezeigt.',
    fr: 'La liste des fonctions affiche toutes les fonctions du fichier actif (ou projet) Vous pouvez double-cliquer sur le nom d\'une fonction pour accéder à sa définition dans le fichier source.<br><br> Si la Liste des fonctions n\'est pas visible, activez-la en appuyant sur <strong>F8</strong>.  Vous pouvez cliquer avec le bouton droit sur la Liste des fonctions pour accéder aux options.',
    es: 'La lista de funciones muestra todas las funciones del archivo (o proyecto) activo. Puede hacer doble clic en un nombre de función de esta lista para saltar a su definición en el archivo de origen.<br><br> Si no ve la lista de funciones, puede habilitarla rápidamente presionando <strong>F8</strong>.  Puede hacer clic con el botón derecho en la lista para acceder a sus funciones.',
    it: 'L\'Elenco Funzioni visualizza tutte le funzioni nel file (o progetto) attivo. È possibile cliccare con il tasto destro del mouse sul nome della funzione in questo elenco per passare alla relativa definizione nel proprio file sorgente.<br><br> Se la funzione che stai cercando non è presente nell\'Elenco Funzioni, l\'attivazione rapida si può eseguire premendo <strong>F8</strong>.  È possibile cliccare con il tasto destro del mouse sull\'Elenco Funzioni per accedere alle relative opzioni.',
    ko: '함수 목록에는 현재 파일 (또는 프로젝트)의 모든 함수가 표시됩니다. 이 목록에서 함수 이름을 더블클릭해서 소스 파일에 있는 해당 함수의 정의로 이동할 수 있습니다.<br><br> 함수 목록이 표시되지 않는 경우 <strong>F8</strong>을 누르면 바로 나타납니다.  함수 목록을 마우스 오른쪽 버튼으로 클릭하면 옵션이 나타납니다.',
    jp: '関数リストには、現在のファイル（またはプロジェクト）における関数がすべて表示されます。リスト内の関数名をダブルクリックすることにより、ソースファイル内の関数定義にジャンプすることができます。<br><br>関数リストが表示されない場合は、［<strong>F8</strong>］ キーを押すだけで有​​効にすることができます。関数リストのオプションにアクセスする場合は、リストの上で右クリックします。',
    ch: '“功能”列表显示活动文件（或项目）中的所有功能。您可以双击本列表中的一个功能名称，跳到其在您的源文件中的定义。<br><br>如果您没看到“功能”列表，则您可以通过按 <strong>F8</strong> 键迅速启用“功能”列表。您可以右键单击“功能”列表以访问其选项。',
    cht: '「功能清單」可顯示作用中檔案（或專案）內的所有功能。您可以在此清單中按兩下功能名稱以跳到其在您來源檔案內的定義。<br><br>如果您沒有&#39;看到「功能清單」，您可以按下<strong>F8</strong>快速啟用它。您可以在「功能清單」上按一下右鍵以存取其選項。'
  },

  f_ScriptingHead: {
    en: 'Automate your tasks with scripting',
    de: '<span style="font-size: 90%;">Aufgaben mit Skripten automatisieren</span>',
    fr: '<span style="font-size: 85%;">Automatisez vos t&acirc;ches &agrave; l\'aide de scripts</span>',
    es: 'Automatizar tareas con Scripts',
    it: '<span style="font-size: 85%;">Automatizza le tue procedure con gli Script</span>',
    ko: '힘들지 않게 지능적으로 작업하고 스크립트로 일을 자동화 합니다.',
    jp: 'スクリプトであなたの仕事をオートメーション化する',
    ch: '利用脚本自动化您的工作。',
    cht: '利用指令碼自動化你的工作。'
  },

  f_Scripting: {  // **** NEEDS TRANSLATION ****
    en: 'Scripting combines the flexibility of Javascript with the power of  <span class="appname"></span> to provide a dynamic method for automating your tasks. <br><br>Want to learn more about scripting? Help provides full documentation, and there are plenty of sample scripts available for download at the link below. After you write your script, simply load it into the Script List in the <strong>Scripting</strong> menu, and you&#39;re ready to play it!',
    de: 'Skripting kombiniert die Flexibilität von Javascript mit der Leistung von  <span class="appname"></span>, um eine dynamische Methode zur Automatisierung Ihrer Aufgaben bereitzustellen. <br><br> Wollen Sie mehr über Skripting erfahren? Die Hilfe bietet die volle Dokumentation und es gibt zahlreiche Beispielskripts, die von dem nachfolgenden Link heruntergeladen werden können. Laden Sie Ihr Skript nach dem Schreiben einfach in die Skriptliste im Menü <strong>Skripting</strong> und Sie können es ausführen!',
    fr: 'Les scripts combinent la souplesse de Javascript avec la puissance de <span class="appname"></span> pour offrir une méthode d\'automatisation des tâches dynamique. <br><br> En savoir plus sur les scripts ? L\'aide propose une documentation complète, et de nombreux exemples de scripts sont disponibles en téléchargement à partir du lien ci-dessous. Après avoir écrit le script, chargez-le simplement dans la liste des scripts dans le menu <strong>Scripts</strong> ; et vous êtes prêt à vous en servir !',
    es: 'La automatización mediante scripts combina la flexibilidad de JavaScript con la potencia de <span class="appname"></span> para proporcionar un método dinámico de automatizar sus tareas. <br><br> ¿Desea más información sobre la automatización con scripts? La ayuda le ofrece información completa y, en el enlace siguiente, encontrará muchos scripts de muestra disponibles para su descarga. Después de escribir el script, solo tiene que cargarlo en la lista de scripts del menú <strong>Automatización</strong> y ya estará listo.',
    it: 'Lo scripting combina la flessibilità dello Javascript con la potenza di  <span class="appname"></span> per fornire un metodo dinamico per automatizzare i propri compiti. <br><br> Desideri sapere di più sullo scripting? La funzione Aiuto fornisce una documentazione completa e sono disponibili anche una serie di script campione da scaricare dal link di seguito. Dopo aver digitato lo script, caricarlo semplicemente nell\'Elenco script nel menu <strong>Scripting</strong> e sarai pronto per eseguirlo!',
    ko: '스크립팅에는 자바스크립트의 유연성과 <span class="appname"></span>의 강력한 기능을 결합하여 작업의 자동화를 위한 동적 방법을 제공합니다. <br><br> 스크립팅에 대해 더 자세히 알고 싶으십니까? 완전 문서화를 지원하며, 아래의 링크에서 다수의 샘플 스크립트를 다운로드할 수 있습니다. 스크립트를 작성한 후 <strong>스크립팅</strong> 메뉴에서 스크립트 목록에 로드하면, 재생 준비가 완료됩니다!',
    jp: 'JavaScriptの柔軟性に<span class="appname"></span>の能力を組み合わせたスクリプティングでは、作業を動的に自動化することができます。<br><br>スクリプトについて詳しく知りたい場合は：ヘルプでは、すべての資料が参照できます。下記のリンクでは、多くのサンプルスクリプトをダウンロードすることができます。スクリプトを記述した後は、<strong>スクリプトメニュー</strong>からスクリプトリストに読み込ませるだけで再生準備が整います。',
    ch: '脚本将 Javascript 的灵活性和 <span class="appname"></span> 的强大功能集于一身，提供自动化任务的动态方法。<br><br>想要了解更多有关脚本的信息？“帮助”提供完整文件，并且可在下面的链接中下载大量脚本样例。撰写脚本后，只需在<strong>脚本</strong>菜单中将其加载到“脚本列表”，即可使用！',
    cht: '指令碼結合了 Javascript 的彈性與<span class="appname"></span>的強大功能，可為您的任務自動化提供一種動態方法。<br><br>想要進一步學習有關指令碼？「說明」提供完整的文件，且以下連結有許多範例指令碼可供下載當您撰寫了指令碼之後，只需要在<strong>指令碼</strong>選單中載入「指令碼」清單即可立即&#39;使用！'
  },

  f_XMLHead: {  // **** NEEDS TRANSLATION ****
    en: 'Handle XML files with ease',
    de: 'Mühelos mit XML-Dateien umgehen',
    fr: 'Manipuler facilement les fichiers XML',
    es: 'Controlar fácilmente archivos XML',
    it: 'Gestisci file XML con facilità',
    ko: '간편하게 XML 파일 처리',
    jp: 'XMLファイルの簡単処理',
    ch: '轻松处理 XML 文件',
    cht: '輕鬆處理 XML 檔案'
  },

  f_XML: {  // **** NEEDS TRANSLATION ****
    en: 'The XML Manager (available in <strong>View</strong> -> <strong>Views/Lists</strong>) allows you to quickly navigate, browse, and modify XML in a tree-style view. <br><br>You can also tidy your XML or reformat a long string of XML data into readable indented lines via the <strong>Format </strong>menu.',
    de: 'Der XML Manager (verfügbar unter <strong>Ansicht</strong> -> <strong>Ansichten/Listen</strong>) ermöglicht das schnelle Navigieren, Durchsuchen und Ändern von XML in einer Strukturansicht. <br><br> Sie können im Menü <strong>Format </strong> Ihr XML auch säubern oder eine lange Zeichenkette von XML-Daten in lesbare eingezogene Zeilen umformatieren.',
    fr: 'Le gestionnaire XML (disponible dans in <strong>Afficher</strong> -> <strong>Vues/Listes</strong>) permet de naviguer, parcourir et modifier rapidement les fichiers XML dans une arborescence. <br><br> Vous pouvez également nettoyer votre XML ou reformater une chaîne de données XML longue en des lignes en retrait lisibles au moyen du menu <strong>Format </strong>.',
    es: 'El Gestor XML (disponible en <strong>Ver</strong> -> <strong>Vistas/Listas</strong>) le permite desplazarse, examinar y modificar rápidamente XML en una vista de tres estilos. <br><br> También puede ordenar el XML o reformatear una cadena larga de datos XML en líneas indentadas legibles con el menú <strong>Formato</strong>.',
    it: 'Il XML Manager (disponibile in <strong>Visualizza</strong> -> <strong>Visualizzazioni/Elenco</strong>) consente all\'utente di navigare, sfogliare e modificare XML in una visualizzazione ad albero. <br><br> È possibile inoltre riordinare il proprio XML oppure riformattare una stringa lunga di dati XML in righe leggibili rientranti attraverso il menu <strong>Formatta </strong>.',
    ko: 'XML 관리자(<strong>보기</strong> -> <strong>보기/목록</strong>에서 사용 가능)를 이용해서 트리 구조 보기에서 XML을 검색, 찾아보기, 수정할 수 있습니다. <br><br> 또한 <strong>형식 </strong>메뉴를 이용해서 XML을 정돈하거나 XML 데이터의 긴 문자열 서식을 들여쓰기된 읽기 가능한 줄에 재설정할 수 있습니다.',
    jp: 'XML Manager（［<strong>表示</strong>］ から ［<strong>表示/リスト</strong>］ を開く）では、XMLの移動、表示、編集をツリー表示で素早く行うことができます。<br><br>また、［<strong>書式</strong>] メニューでは、XMLを整理したり、XMLデータの長い文字列を読みやすいインデント行へ再フォーマットしたりすることができます。',
    ch: 'XML 管理器（在<strong>视图</strong> -> <strong>视图/列表</strong>中提供）可让您在树状结构视图中迅速导航、浏览和修改 XML。<br><br>您也可以精简您的 XML，或者通过<strong>格式</strong>菜单将一长串 XML 数据的格式重排为可读缩进式行。',
    cht: 'XML 管理員（位於<strong>檢視</strong> -> <strong>檢視/清單</strong>）內，可讓您以樹狀結構檢視的方式快速導覽、瀏覽並修改 XML。<br><br>您也可以透過<strong>格式</strong>選單來整理您的 XML 或重新格式化 XML 資料的長字串成為可閱讀的縮排行。'
  },

  f_BookmarksHead: {
    en: 'Simplify your workflow using bookmarks',
    de: 'Den Arbeitsablauf mit Lesezeichen vereinfachen',
    fr: 'All&eacute;gez votre charge de travail en utilisant des marques',
    es: 'Simplifique su ritmo de trabajo usando Marcadores',
    it: 'Semplifica il tuo lavoro utilizzando i Segnalibri',
    ko: '책갈피를 이용하여 작업흐름을 단순화 하십시오',
    jp: 'ブックマークを利用してあなたのワークフローをよりシンプルに',
    ch: '利用标签简化您的工作流程',
    cht: '使用書籤來簡化你的工作流程'
  },

  f_Bookmarks: {
    en: 'Bookmarks are a convenient way to save an important location in your file or code. If you want to set a bookmark simply press <strong>Ctrl</strong> + <strong>F2</strong>; to jump to the next available bookmark press <strong>F2</strong>; to jump to the previous bookmark press <strong>ALT</strong> + <strong>F2</strong>. <br><br>You can use the Bookmark Viewer to quickly see (and navigate to) the bookmarks in the active document as well as the bookmarks in all open files.',
    de: '<span style="font-size: 90%; line-height: 90%;">Mit Lesezeichen kann man auf einfache Weise bestimmte Stellen in Ihrer Datei oder Ihrem Code &raquo;speichern&laquo;. Um ein Lesezeichen zu setzen, dr&uuml;ckt man <strong>STRG</strong>&nbsp;+&nbsp;<strong>F2</strong>; um vorw&auml;rts im Text zum n&auml;chsten Lesezeichen zu springen: <strong>F2</strong>; r&uuml;ckw&auml;rts: <strong>ALT</strong>&nbsp;+&nbsp;<strong>F2</strong>. <br><br>Die Lesezeichenansicht bietet eine &Uuml;bersicht und auch einen Schnellzugriff auf Lesezeichen im aktuellen Dokument sowie in allen offenen Dateien. Lesezeichen k&ouml;nnen direkt aus der Lesezeichenansicht heraus bearbeitet oder gel&ouml;scht werden.</span>',
    fr: '<span style="font-size: 85%; line-height: 90%;">Les marques sont un moyen pratique de &laquo;&nbsp;m&eacute;moriser&nbsp;&raquo; un emplacement important dans votre fichier ou votre code.  Pour d&eacute;finir une marque, appuyez simplement sur <strong>CTRL</strong> + <strong>F2</strong> ; appuyez sur <strong>F2</strong> pour passer &agrave; la marque suivante ; appuyez sur <strong>ALT</strong> + <strong>F2</strong> pour retourner &agrave; la marque pr&eacute;c&eacute;dente. <br><br>Vous pouvez utiliser la Visionneuse des marques pour jeter un oeil rapide sur les marques du document actif ainsi que les marques de tous les fichiers ouverts. Par ailleurs, vous pouvez &eacute;galement utiliser la Visionneuse des marques pour &eacute;diter et supprimer des marques.',
    es: 'Los Marcadores son un modo conveniente de "salvar" una posición importante en su archivo o código. Si desea crear un marcador pulse <strong>CTRL</strong>+<strong>F2</strong>, para ir al siguiente marcador pulse <strong>F2</strong>, para ir al marcador anterior pulse <strong>ALT</strong>+<strong>F2</strong>. <br><br>Puede usar el Visor de Marcadores para ver rápidamente (y navegar por ellos) los marcadores del documento activo, y los de todos los archivos abiertos. Además también puede usar el Visor de Marcadores para editar y suprimir marcadores.',
    it: 'I Segnalibri sono un modo comodo per "salvare" posizioni importanti nei tuoi file o nel tuo codice.  Se vuoi impostare un segnalibro premi semplicemente <strong>CTRL</strong> + <strong>F2</strong>, o salta al segnalibro successivo premento <strong>F2</strong>, o a quello precedente premendo <strong>ALT</strong> + <strong>F2</strong>. <br><br>Puoi utilizzare il Visualizzatore Segnalibri per visualizzare velocemente (e navigare tra) i segnalibri nel documento attivo o nei file aperti. Inoltre puoi utilizzare il Visualizzatore Segnalibri per modificare e cancellare i segnalibri.',
    ko: '<span style="font-size: 95%">책갈피를 이용하면 편리하게 파일 또는 코드의 중요한 위치를 저장할 수 있습니다. 책갈피를 저장하려면 간단히 <strong>CTRL</strong> + <strong>F2</strong>를 누르면 되고, 다음 책갈피로 이동하려면 <strong>F2</strong>를 그리고 이전 책갈피로 이동하려면 <strong>ALT</strong> + <strong>F2</strong>를 누르면 됩니다. <br><br>책갈피보기를 사용하면 현재문서와 지금 열려있는 모든 파일의 책갈피를 빠르게 볼 수 있습니다. 더 나아가, 책갈피 보기를 사용하여 책갈피를 편집하고 삭제할 수 있습니다.</span>',
    jp: '<span style="font-size: 88%; line-height: 90%;">ブックマークはあなたのファイル、またはコード中の重要な場所を "保存" するのに非常に便利です。ブックマークを設定するには、<strong>CTRL</strong> + <strong>F2</strong>を押してください、次のブックマークに移動するには、<strong>F2</strong>を、また前のブックマークに移動するには、<strong>ALT</strong> + <strong>F2</strong>を押してください。 <br><br>ブックマークビュワーを利用して、アクティブドキュメント中のブックマークをすばやく見る（移動する）ことができます。またこれは同様に全ての開いているファイルに対しても有効です。さらに、ブックマークビュワーを利用して、ブックマークを編集したり、削除したりすることも可能です。</span>',
    ch: '标签是一个方便的方法“保存”文件和编码重要的位置。如果您需要做标签，按 <strong>CTRL</strong> + <strong>F2</strong>；跳到下一个标签，按 <strong>F2</strong>；跳到上一个标签，按 <strong>ALT</strong> + <strong>F2</strong>。 <br><br>您可以用标签检视器快速检视（和导航到）在活跃文件内的标签， 和在所有打开的文件的标签。',
    cht: '書籤是一種方便的方式，用來在你的檔案或程式碼中儲存一個重要的位置。如果你要設定一個書籤只需按下 <strong>CTRL</strong> + <strong>F2</strong>；要跳到下一個可用的書籤}，按下 <strong>F2</strong>；要跳到上一個書籤}，按下 <strong>ALT</strong> + <strong>F2</strong>。 <br><br>你可以使用書籤檢視器以快速檢視 (及導覽) 作用中文件的書籤，同時也可以在所有開啟的檔案中使用書籤。'
  },

  f_HTMLHead: {
    en: 'Visually inspect your HTML with the integrated browser preview',
    de: '&Uuml;berpr&uuml;fen Sie HTML-Text visuell in der integrierten Browser-Ansicht',
    fr: 'Contr&ocirc;lez visuellement votre HTML &agrave; l\'aide de l\'aper&ccedil;u navigateur int&eacute;gr&eacute;',
    es: 'Revise visualmente su HTML con la vista previa de su navegador',
    it: 'Analizza visivamente il tuo HTML con l\'anteprima browser integrata',
    ko: '통합 브라우저미리보기로 HTML파일의 내용을 확인할 수 있습니다',
    jp: 'あなたのHTMLを拡張されたブラウザプレビューで視覚的に編集できます',
    ch: '利用综合浏览器预览检查您的 HTML',
    cht: 'HTML 預覽'
  },

  f_HTML: {
    en: 'Open and edit your HTML file, then toggle the browser preview from the <strong>main toolbar</strong> or from the <strong>View</strong> menu.<br><br>When you&#39;re done previewing your changes, toggle the browser preview off... It&#39;s that easy!',
    de: 'Schreiben Sie einfach HTML-Text, dann mit &raquo;Browser-Ansicht ein/aus&laquo; (in der <strong>Hauptsymbolleiste</strong> oder &uuml;ber das Men&uuml; <strong>Ansicht</strong>) zur HTML-Ansicht wechseln. Der gleiche Befehl bringt Sie nachher wieder in den Bearbeitungsmodus - so einfach ist das!',
    fr: 'Il vous suffit d\'&eacute;diter votre fichier HTML puis de basculer dans la &laquo;&nbsp;Vue navigateur&nbsp;&raquo; &agrave; partir de la barre d\'outils principale ou du menu <strong>Affichage</strong>. Une fois que vous avez pr&eacute;visualis&eacute; vos modifications, refermez la vue navigateur - D\'une simplicit&eacute; enfantine !',
    es: 'Simplemente edite su html, pulse en "Previsualizar en Navegador" en la <strong>barra principal</strong> o en el menú <strong>Ver</strong>. Cuando haya previsualizado los cambios, desactive la Previsualización en Navegador - ¡Así de fácil!',
    it: 'Semplicemente, modifica il tuo file html, poi attiva "Anteprima Browser" dalla <strong>barra strumenti principale</strong> o dal menu <strong>Visualizza</strong>. Quando hai finito, disabilita l\'Anteprima Browser - più semplice di così!',
    ko: '간단히 HTML 파일을 편집하고 보기메뉴의 도구바에서 브라우저 미리보기로 전환하면 됩니다. 미리보기로 변경사항을 확인한 후 미리보기를 종료하면 됩니다. 매우 쉽습니다.',
    jp: 'シンプルに、ｈｔｍｌファイルを編集して、 ツールバーの"ブラウザで表示" アイコンをクリックするか、表示メニューから選択してください。変更点のプレビューがすんだなら、ブラウザプレビューを終了を押してください。どうでしょう？非常に簡単かつシンプルです！',
    ch: '简单地编辑 html 文件，从主要的工具栏或从检视菜单中转换“浏览器预览”。如果您完成预览您的改动，转换浏览器预览到关状态... 很容易!',
    cht: '只需簡單的編輯你的} html 檔案，然後從主工具列或檢視功能表中切換「瀏覽器預覽」。當你預覽完成你的變更，再將瀏覽器預覽關閉即可...非常的容易！'
  },

};