/*

This file gets the content and settings for IPMs from ipm-settings.js

*/

//--------------------------------------
// Execution
//--------------------------------------

window.onload = function() {

  var daysLeft  = getParam('daysleft');
  var totalDays = getParam('totaldays');
  var percent   = 0;
  var lang      = getParam('ln');
  var prod      = getParam('prod') ? getParam('prod') : 'uew';

  /* ----------------------------------
      1) Set title text
  ------------------------------------- */
  if (daysLeft > 15) {
    replaceInnerHTML('title', conf_title_trial_mode);
  } else if (daysLeft > 3) {
    replaceInnerHTML('title', conf_title_trial_expiring);
  } else if (daysLeft > 0) {
    replaceInnerHTML('title', conf_title_almost_expired);
  } else {
    replaceInnerHTML('title', conf_title_trial_expired);
  }

  /* ----------------------------------
      2) Set "days remaining" text
  ------------------------------------- */
  if (daysLeft > 0) {
    var daysLeftText = conf_days_left + "<strong>" + daysLeft + "</strong>";
    replaceInnerHTML('trial-days-text', daysLeftText);
  } else {
    replaceInnerHTML('trial-days-text', conf_no_days_left);
  }

  /* ----------------------------------
      3) Build progress bar
  ------------------------------------- */
  // Set % of trial left if values are numbers
  if ((daysLeft && !isNaN(daysLeft)) && (totalDays && !isNaN(totalDays))) {
    var percent   = (daysLeft / totalDays) * 100;
  }

  // Set borders
  switch(percent) {
    case 0:
      progressBorder = ' border-width: 0;';
      break;
    case 100:
      progressBorder = ' border-right: 1px solid;';
      break;
    default:
      progressBorder = '';
  }

  var progressBarHTML = '<div id="trial-progress"><span style="width: ' + percent + '%;' + progressBorder + '"></span></div>';

  replaceInnerHTML('progress-bar', progressBarHTML);

  /* ----------------------------------
      4) Set dynamic IPM content
  ------------------------------------- */
  if (conf_day_content[daysLeft]) {
    replaceInnerHTML('ipm-dynamic', conf_day_content[daysLeft]);
  } else {
    replaceInnerHTML('ipm-dynamic', conf_day_content.d);
  }

  /* ----------------------------------
      5) Localize strings
  ------------------------------------- */
  if (!lang || loc_supportedLangs.indexOf(lang) == -1) {
    lang = "en";
  }

  for (var str in loc_langs) {
    var uid = 'l_' + str;
    var els = document.querySelectorAll('.' + uid);
    for (var i = 0; i < els.length; i++) {
      els[i].innerHTML = loc_langs[str][lang];
    }
  }

  /* ----------------------------------
      6) Set app name & prices
  ------------------------------------- */
  // app name
  var app_spans = document.querySelectorAll('span.appname');
  for (var i = 0; i < app_spans.length; i++) {
    app_spans[i].innerHTML = conf_apps[prod].appname;
  }

  // bundled app name (UltraCompare or UC Mobile)
  var bdl_spans = document.querySelectorAll('span.bdlname');
  for (var i = 0; i < bdl_spans.length; i++) {
    bdl_spans[i].innerHTML = conf_apps[prod].bdlname;
  }

  // app abbreviation
  var abbr_spans = document.querySelectorAll('span.appabbr');
  for (var i = 0; i < abbr_spans.length; i++) {
    abbr_spans[i].innerHTML = conf_apps[prod].appabbr;
  }

  // bundled app (UC or UCm) abbreviation
  var bdlabbr_spans = document.querySelectorAll('span.bdlabbr');
  for (var i = 0; i < bdlabbr_spans.length; i++) {
    bdlabbr_spans[i].innerHTML = conf_apps[prod].bdlabbr;
  }
  
  // app box shot
  var appbox_spans = document.querySelectorAll('img.appbox');
  for (var i = 0; i < appbox_spans.length; i++) {
    appbox_spans[i].src = 'images/' + conf_apps[prod].appbox;
  }
  
  // bundle box shot
  var bdlbox_spans = document.querySelectorAll('img.bdlbox');
  for (var i = 0; i < bdlbox_spans.length; i++) {
    bdlbox_spans[i].src = 'images/' + conf_apps[prod].bdlbox;
  }
  
  // prices
  for (var pr in conf_apps[prod].prices) {
    var p_els = document.querySelectorAll('.price.' + pr);
    for (var i = 0; i < p_els.length; i++) {
      p_els[i].innerHTML = conf_apps[prod].prices[pr];
    }
  }

  /* ----------------------------------
      7) Localize app prices
  ------------------------------------- */
  // German
  if (lang == 'de') {
    var lp_els = document.querySelectorAll('.price');
    for (var i = 0; i < lp_els.length; i++) {
      var origPrice = lp_els[i].innerHTML;
      lp_els[i].innerHTML = origPrice.replace(".", ",");
    }
  }

  // French
  if (lang == 'fr') {
    var lp_els = document.querySelectorAll('.price');
    for (var i = 0; i < lp_els.length; i++) {
      var origPrice = lp_els[i].innerHTML;
      var refPrice = origPrice.replace(".", ",");
      var refPrice = refPrice.replace("$", "");
      // conditional exclusion for long bundle price
      if (lp_els[i].className.indexOf('pnewBdlR') < 1) {
        var refPrice = refPrice + " $";
      }
      lp_els[i].innerHTML = refPrice;
    }
  }
  
  if (lang == 'ch' && prod == "uew") {
    var chs_els = document.querySelectorAll('.price.pnew');
    for (var i = 0; i < chs_els.length; i++) {
      chs_els[i].innerHTML = '150元人民币';
    }
  }
    

  /* ----------------------------------
      8) Populate links
  ------------------------------------- */
  for (var url in conf_apps[prod].urls) {
    if (a = document.getElementById(url)) {
      // Construct the URL for the link
      var new_url = conf_url_base  + lang + "/" + conf_apps[prod].urls[url];
      // Add tracking code
      new_url += "?utm_source=" + conf_apps[prod].appname + "&utm_medium=ipm&utm_campaign=" + daysLeft;
      a.setAttribute('href', new_url);
    }
  }

  //window.alert("OnLoad Complete");
  window.location.href = "#ONLOADCOMPLETE";
}