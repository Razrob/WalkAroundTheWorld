mergeInto(LibraryManager.library,
{
	InitSDK_Internal: function (playerPhotoSize, scopes)
	{
		InitSDK(UTF8ToString(playerPhotoSize), scopes);
	},
	
	OpenAuthDialog: function (playerPhotoSize, scopes)
	{
		OpenAuthDialog(UTF8ToString(playerPhotoSize), scopes);
	},
	
	SaveYG: function (jsonData, flush)
	{
		SaveCloud(UTF8ToString(jsonData), flush);
	},
	
	LoadYG: function ()
	{
		LoadCloud();
	},
	
	FullAdShow: function ()
	{
		FullAdShow();
	},

    RewardedShow: function (id)
	{
		RewardedShow(id);
	},
	
	LanguageRequestInternal: function ()
	{
		LanguageRequest();
	},
	
	RequestingEnvironmentData: function()
	{
		RequestingEnvironmentData();
	},	

	ReviewInternal: function()
	{
		Review();
	},
	
	BuyPaymentsInternal: function(id)
	{
		BuyPayments(UTF8ToString(id));
	},
	
	GetPaymentsInternal: function()
	{
		GetPayments();
	},
	
	ConsumePurchaseInternal: function(id)
	{
		ConsumePurchase(UTF8ToString(id));
	},
	
	ConsumePurchasesInternal: function()
	{
		ConsumePurchases();
	},
	
	PromptShowInternal: function()
	{
		PromptShow();
	},
	
	StickyAdActivityInternal: function(show)
	{
		StickyAdActivity(show);
	},
	
	GetURLFromPage: function () {
        var returnStr = (window.location != window.parent.location) ? document.referrer : document.location.href;
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
		
        return buffer;
    },
	
	OpenURL: function (url) {
		var a = document.createElement("a");
		a.setAttribute("href", UTF8ToString(url));
		a.setAttribute("target", "_blank");
		a.click();
	}
});

var FileIO = {

  SaveToLocalStorage : function(key, data) {
	try {
		localStorage.setItem(UTF8ToString(key), UTF8ToString(data));
	}
	catch (e) {
		console.error('Save to Local Storage error: ', e.message);
	}
  },

  LoadFromLocalStorage : function(key) {
    var returnStr = localStorage.getItem(UTF8ToString(key));
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  RemoveFromLocalStorage : function(key) {
    localStorage.removeItem(UTF8ToString(key));
  },

  HasKeyInLocalStorage : function(key) {
	try {
		if (localStorage.getItem(UTF8ToString(key))) {
		  return 1;
		}
		else {
		  return 0;
		}
	}
	catch (e) {
		console.error('Has key in Local Storage error: ', e.message);
		return 0;
	}
  }
};

mergeInto(LibraryManager.library, FileIO);