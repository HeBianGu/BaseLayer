/* -----------------------------
URLS
----------------------------- */
var conf_url_base = 'http://www.ultraedit.com/redirects/registration/';

/* -----------------------------
Conditional strings
----------------------------- */


// Progress bar text
var conf_days_left = '<span class="l_trialDaysText">Days left in free trial:</span> ';
var conf_no_days_left = '<span class="l_noTrialDaysText">Please purchase a license to continue using <span class="appname"></span>.</span>';

// Title: In trial mode
var conf_title_trial_mode = hereDoc(function() {/*!
          <span id="trialText" class="l_trialModeText">
            <span class="appname"></span> is running in trial mode.
          </span>
*/});

// Title: Trial is expiring
var conf_title_trial_expiring = hereDoc(function() {/*!
          <span id="trialText" class="l_trialExpiring">
            Your free trial is expiring.
          </span>
*/});

// Title: Trial about to expire
var conf_title_almost_expired = hereDoc(function() {/*!
          <span id="trialExpiredText" class="l_trialAlmostExpiredText">
            Your free trial has almost expired.
          </span>
*/});

// Title: Trial expired
var conf_title_trial_expired = hereDoc(function() {/*!
          <span id="trialExpiredText" class="l_trialExpiredText">
            Your free trial has expired.
          </span>
*/});




/* -----------------------------
Dynamic content blocks
----------------------------- */

// UE content block
var conf_content_ue = hereDoc(function() {/*!
      <div id="ipm-content-inner">

        <!-- box shot / multi-platform-->
        <div class="left-col-box">
          <img src="images/box-ue.png" alt="" style="margin-bottom: 12px;" class="appbox">
          <img src="images/multi-platform.png" alt="">
        </div>

        <!-- benefits text -->
        <div class="right-col-benefits">

          <p>
            <span class="l_bftHead">
              Your personal license includes:
            </span>
          </p>

          <div class="benefits-list">

            <img src="images/check.png" alt="*">
            <span class="l_bftMultiPlatform">
              The <strong>Windows</strong>, <strong>Mac</strong>, and <strong>Linux</strong> versions
            </span>
            <br>

            <img src="images/check.png" alt="*">
            <span class="l_bftThreeInstalls">
              Installation on up to 3 machines
            </span>
            <br>

            <img src="images/check.png" alt="*">
            <span class="l_bftFreeUpgrades">
              Free upgrades for a year
            </span>
            <br>

            <img src="images/check.png" alt="*">
            <span class="l_bftTechSupport">
              Unlimited lifetime tech support
            </span>

          </div>

          <p>
            <span class="l_worldsBest">
              <span class="appname"></span>: The world&#39;s #1 text editor.<br>
              Preferred by millions!
            </span>
          </p>

        </div>

      </div>
*/});

// UE/UC promo content block
var conf_content_ueuc = hereDoc(function() {/*!
      <div class="darkbg">
        <div id="ipm-content-inner" class="darkbg">

          <h2 class="attention">
            <span class="l_bundleHeadline">
            </span>
          </h2>

          <div class="left-col-box" styl>

            <img src="images/box-ueuc.png" alt="" style="margin: 12px 0 8px 0;" class="bdlbox"><br>

            <div class="prices">

              <span class="l_ueucPriceRetailText">
                Retail:
              </span>
              <span class="price pnewBdlR l_ueucPriceRetailAmt">
              </span>

              <br>

              <span class="l_ueucPriceText">
                You pay:
              </span>
              <span class="price pnewBdl l_ueucPriceAmt">
              </span>

            </div>

            <img src="images/multi-platform.png" alt="" style="margin-top: 12px;">

          </div>

          <ul class="benefitList">

            <li>&bull;
              <span class="l_ueucBft1">
                Integrated with UE
              </span>
            </li>

            <li>&bull;
              <span class="l_ueucBft2">
                Integrate with version control
              </span>
            </li>

            <li>&bull;
              <span class="l_ueucBft3">
                Diff up to 3 files/folders
              </span>
            </li>

            <li>&bull;
              <span class="l_ueucBft4">
                Compare Word docs, PDFs
              </span>
            </li>

            <li>&bull;
              <span class="l_ueucBft5">
                Sync local to FTP
              </span>
            </li>

            <li>&bull;
              <span class="l_ueucBft6">
                Compare & preview HTML
              </span>
            </li>

            <li>&bull;
              <span class="l_ueucBft7">
                Merge differences
              </span>
            </li>

            <li>&bull;
              <span class="l_ueucBft8">
                Save compare sessions
              </span>
            </li>

          </ul>

          <div class="savingsText">
            <span class="l_ueucSavingsPercent">
              Save 50%
            </span>
            <br>
            <span class="l_ueucSavingsText">
              on UC when you bundle it with UE!
            </span>
          </div>

          <!-- Buy new button -->
          <a href="#" id="buynewBdl" target="_blank" class="button">
            <span class="l_buyueucNew" style="font-size: 85%;">
              Buy UE/UC new:
            </span>
            <strong>
              <span class="price pnewBdl" style="color: #000000;">
              </span>
            </strong>
          </a>

          <!-- Upgrade button -->
          <a href="#" id="buyupg2Bdl" target="_blank" class="button">
            <span class="l_upgradeueuc" style="font-size: 85%;">
              Upgrade UE, add UC:
            </span>
            <strong>
              <span class="price pupg2Bdl">
              </span>
            </strong>
          </a>

        </div>
      </div>
*/});

var conf_content_f_multicaret = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-multi-caret.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_MultiCaretHead">
        Edit multiple places in your file at once.
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_MultiCaret">
        Press <strong>Ctrl</strong>, then <strong>click</strong> on different places where you want to edit. Begin typing. <span class="appname"></span> updates your file in all locations!
        <br>
        <br>
        You can also press <strong>Ctrl</strong> and then <strong>double-click</strong> text or <strong>click and drag</strong> to create multiple selections in the file. Edit, cut, copy, or paste all at once.
        <br>
        <br>
        <strong>Check this out:</strong> You can also quickly create multiple carets at the end of lines by selecting the lines, then <strong>Ctrl</strong> + <strong>clicking</strong> beyond the end of them.
       </span>
      </p>

     </div>
*/});

var conf_content_f_themes = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-themes-graphic.png" class="feat-img" style="margin-left: 18px; margin-top: 24px; margin-right: 18px;">

      <h3 class="feature">
       <span class="l_f_ThemesHead">
        Select your own layout and theme
       </span>
      </h3>

      <p class="feat-desc">
       <img src="images/feat-themes-icon.png" class="img-left">
       <span class="l_f_Themes">
        Want a clean look? Do you prefer multiple windows or no toolbars? Do you like a dark interface? Choose the layout and theme you prefer by clicking the Layout/Theme Selector on the main toolbar.
       </span>
      </p>

      <img src="images/feat-themes-layouts.png" style="margin-top: 8px;">

     </div>
*/});

var conf_content_f_compare = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-compare.png" class="feat-img" style="margin-bottom: 2px;">

      <h3 class="feature">
       <span class="l_f_CompareHead">
        Quickly compare files and folders with <span class="bdlname"></span>
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_Compare">
        <span class="appname"></span> includes <strong><span class="bdlabbr">UC</span> Lite</strong> for basic diff tasks such as comparing files and identifying differences.
        <br>
        <br>
        <strong><span class="bldname"></span></strong> provides a more complete feature set. UC Pro includes 2 and 3-way compare, folder compare and sync, merge, zip archive compare, ignore options, sessions, Word Doc / PDF compare, and more...
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/products/ultracompare.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>

     </div>
*/});

var conf_content_f_find = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-find.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_findReplaceHead">
        Find/replace text across a single file, multiple files, and more
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_findReplace">
        Press Ctrl + F to quickly search for text with Quick Find, or press Ctrl + F a second time to open the main Find dialog.  Ctrl + R opens the Replace dialog.  All find and replace options are available in the Search menu.
        <br>
        <br>
        Advanced find / replace options include regular expressions, find and replace in files, search in column, and much more...
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/find_replace.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>

     </div>
*/});

var conf_content_f_ftp = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-ftp.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_FTPHead">
        Edit files from FTP / SFTP servers
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_FTP">
        Whether you&#39;re using <span class="appname"></span> for development or basic editing, you may need FTP. Set up your FTP account (via the <strong>File</strong> menu) and take advantage of the built-in FTP open and save options as well as the FTP browser.
        <br>
        <br>Need to compare changed files / folders on your server? No problem - use UC Pro&#39;s built in FTP compare. You can even sync up your local / remote directories.
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/configure_ftp.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_column = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-column.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_ColumnHead">
        Column mode and block select
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_Column">
        Column mode allows you to select columns and rows of text as opposed to only selecting rows.
        <br>
        <br>To use column mode, press <strong>Alt</strong> + <strong>C</strong>.  You can also hold down the <strong>Alt</strong> key and <strong>click and drag</strong> to make quick block selections. Type, cut, copy, paste and more - all in column mode!
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/column_mode.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_templates = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-templates.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_TemplatesHead">
        Smart templates / code snippets
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_Templates">
        With smart templates, you quickly insert code snippets (either automatically when you type a keyword or via auto-complete) based upon the type of file you're editing. Also, you can add custom variables to your templates.
        <br>
        <br>
        There are several pre-configured smart templates for most source code languages, and you can also create or modify your own in the <strong>Advanced</strong> menu.
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/smart-templates.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_sort = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-sort.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_SortHead">
        Sort selected text or the entire file
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_Sort">
        Quickly sort a file or selection alphabetially or numerically, ascending or descending. Sort based upon one or more column ranges and optionally remove duplicate entries.  Sort with basic or advanced options.
        <br>
        <br>
        Sorting can help you read log files, sort fields in flat file databases, or organize any text. You can access the sort feature in the <strong>File</strong> menu.
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/advanced_column_based_sort.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_wordfile = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-wordfile.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_WordfileHead">
        Add syntax highlighting for other languages by adding wordfiles
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_Wordfile">
        <span class="appname"></span> natively supports syntax highlighting for 14 commonly-used coding languages.  However, you can add a new language by simply downloading and saving a new wordfile into the correct location. Over 600 extra wordfiles are available for download from our site!
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/add_a_wordfile.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_findreplacefiles = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-findinfiles.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_FindReplaceFilesHead">
        Find and Replace in Files
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_FindReplaceFiles">
        The Find in Files/Replace in Files options are accessible under the Search menu. These powerful search options allow you to search for strings or text within multiple files contained in a directory.
        <br>
        <br>
        With Find/Replace in Files, you have all of the features available to you with Find/Replace plus the option to search subdirectories, Project/Favorite Files, files by type, and more, with advanced ignore options.
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/find_replace.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_functionlist = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-functionlist.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_FunctionListHead">
        Quickly jump to function definitions with the Function List
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_FunctionList">
        The Function List displays all functions in the active file (or project). You can double-click a function name in this list to jump to its definition in your source file.
        <br>
        <br>
        If you don't see the Function List, you can quickly enable it by pressing <strong>F8</strong>.  You can right-click on the Function List to access its options.
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/parse_source_code_with_the_function_list.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_scripting = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-scripting.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_ScriptingHead">
        Automate your tasks with scripting
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_Scripting">
        Scripting combines the flexibility of Javascript with the power of <span class="appname"></span> to provide a dynamic method for automating your tasks. 
        <br>
        <br>
        Want to learn more about scripting? Help provides full documentation, and there are plenty of sample scripts available for download at the link below. After you write your script, simply load it into the Script List in the <strong>Scripting</strong> menu, and you're ready to play it!
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/downloads/extras/macros-scripts.html#scripts" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_xml = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-xml.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_XMLHead">
        Handle XML files with ease
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_XML">
        The XML Manager (available in <strong>View</strong> -> <strong>Views/Lists</strong>) allows you to quickly navigate, browse, and modify XML in a tree-style view.
        <br>
        <br>
        You can also tidy your XML or reformat a long string of XML data into readable indented lines via the <strong>Format </strong>menu.
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/parsing_xml_files.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_bookmarks = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-bookmarks.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_BookmarksHead">
        Simplify your workflow using bookmarks
       </span>
      </h3>

      <p class="feat-desc">
       <span class="l_f_Bookmarks">
        Bookmarks are a convenient way to "save" an important location in your file or code. If you want to set a bookmark simply press <strong>Ctrl</strong> + <strong>F2</strong>; to jump to the next available bookmark press <strong>F2</strong>; to jump to the previous bookmark press <strong>ALT</strong> + <strong>F2</strong>.
        <br>
        <br>
        You can use the Bookmark Viewer to quickly see (and navigate to) the bookmarks in the active document as well as the bookmarks in all open files.
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/bookmarks.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});

var conf_content_f_html = hereDoc(function() {/*!
     <div class="darkbg">

      <img src="images/feat-html.png" class="feat-img">

      <h3 class="feature">
       <span class="l_f_HTMLHead">
        Visually inspect your HTML with the integrated browser preview
       </span>
      </h3>

      <p class="feat-desc">
       <img src="images/feat-html-icon.png" class="img-left">
       <span class="l_f_HTML">
        Open and edit your HTML file, then toggle the browser preview from the <strong>main toolbar</strong> or from the <strong>View</strong> menu.
        <br>
        <br>
        When you're done previewing your changes, toggle the browser preview off... It's that easy!
       </span>
       <br>
       <br>
       <a href="http://www.ultraedit.com/support/tutorials_power_tips/ultraedit/integrated_html_preview.html" target="_blank">
        <span class="l_learnMore">
         Click to learn more
        </span>
       </a>
      </p>
      
     </div>
*/});


// Object with each day and what its content will be.
conf_day_content = {
   d : conf_content_ue,  // Default
  30 : conf_content_ue,
  29 : conf_content_f_multicaret,
  28 : conf_content_f_themes,
  27 : conf_content_f_compare,
  26 : conf_content_ueuc,
  25 : conf_content_f_find,
  24 : conf_content_f_ftp,
  23 : conf_content_f_column,
  22 : conf_content_ueuc,
  21 : conf_content_ue,
  20 : conf_content_f_templates,
  19 : conf_content_f_sort,
  18 : conf_content_f_wordfile,
  17 : conf_content_ueuc,
  16 : conf_content_f_scripting,
  15 : conf_content_f_findreplacefiles,
  14 : conf_content_f_functionlist,
  13 : conf_content_ue,
  12 : conf_content_ueuc,
  11 : conf_content_ueuc,
  10 : conf_content_f_html,
   9 : conf_content_f_xml,
   8 : conf_content_f_bookmarks,
   7 : conf_content_ueuc,
   6 : conf_content_ue,
   5 : conf_content_ue,
   4 : conf_content_ueuc,
   3 : conf_content_ue,
   2 : conf_content_ueuc,
   1 : conf_content_ueuc,
   0 : conf_content_ueuc
};

// Object with app names and properties
conf_apps = {

  uew : {
    appname: 'UltraEdit',
    appabbr: 'UE',
    bdlname: 'UltraCompare',
    bdlabbr: 'UC',
    os     : 'Windows',
    appbox : 'box-ue.png',
    bdlbox : 'box-ueuc.png',
    prices: {
      pnew    : '$79.95',
      pupg    : '$39.95',
      pnewBdl : '$99.95',
      pnewBdlR: '$129.95',
      pupg2Bdl: '$69.95',
    },
    urls: {
      buynew    : 'ue_register.html',
      buyupg    : 'ue_paid_upgrade.html',
      buynewBdl : 'ueuc_bundle_register.html',
      buyupg2Bdl: 'ue2ueuc_bundle_upgrade.html'
    }
  },

  ues : {
    appname: 'UEStudio',
    appabbr: 'UES',
    bdlname: 'UltraCompare',
    bdlabbr: 'UC',
    os     : 'Windows',
    appbox : 'box-ues.png',
    bdlbox : 'box-uesuc.png',
    prices: {
      pnew    : '$99.95',
      pupg    : '$49.95',
      pnewBdl : '$119.95',
      pnewBdlR: '$149.95',
      pupg2Bdl: '$69.95',
    },
    urls: {
      buynew    : 'ues_register.html',
      buyupg    : 'ues_paid_upgrade.html',
      buynewBdl : 'uesuc_bundle_register.html',
      buyupg2Bdl: 'ues2uesuc_bundle_upgrade.html'
    }
  },

  uem : {
    appname : 'UE Mobile',
    appabbr : 'UEm',
    bdlname : 'UC Mobile',
    bdlabbr : 'UCm',
    os      : 'Windows',
    appbox  : 'box-uem.png',
    bdlbox  : 'box-uemucm.png',
    prices: {
      pnew    : '$59.95',
      pupg    : '$29.95',
      pnewBdl : '$89.95',
      pnewBdlR: '$109.95',
      pupg2Bdl: '$54.95',
    },
    urls: {
      buynew    : 'ue3_register.html',
      buyupg    : 'ue3_paid_upgrade.html',
      buynewBdl : 'ue3uc3_bundle_register.html',
      buyupg2Bdl: 'ue32ue3uc3_bundle_upgrade.html'
    }
  }

}


/* -----------------------------
Internally used functions
----------------------------- */
/* Kludgy function to facilitate multi-line strings in Javascript */
function hereDoc(f) {
  return f.toString().
  replace(/^[^\/]+\/\*!?/, '').
  replace(/\*\/[^\/]+$/, '');
}

/* IE9 doesn't support indexOf so we have to create it ourselves */
if (!Array.prototype.indexOf) {
  Array.prototype.indexOf = function (searchElement /*, fromIndex */ ) {
    'use strict';
    if (this == null) {
      throw new TypeError();
    }
    var n, k, t = Object(this),
        len = t.length >>> 0;

    if (len === 0) {
      return -1;
    }
    n = 0;
    if (arguments.length > 1) {
      n = Number(arguments[1]);
      if (n != n) { // shortcut for verifying if it's NaN
        n = 0;
      } else if (n != 0 && n != Infinity && n != -Infinity) {
        n = (n > 0 || -1) * Math.floor(Math.abs(n));
      }
    }
    if (n >= len) {
      return -1;
    }
    for (k = n >= 0 ? n : Math.max(len - Math.abs(n), 0); k < len; k++) {
      if (k in t && t[k] === searchElement) {
        return k;
      }
    }
    return -1;
  };
}

// Gets URL parameters
function getParam(name) {
  return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search)||[,""])[1].replace(/\+/g, '%20'))||null
}

// replace the innerHTML of an element (targeted by ID)
function replaceInnerHTML(replaceID, replaceWith) {
  var e = document.getElementById(replaceID);
  if (e) e.innerHTML = replaceWith; 
}