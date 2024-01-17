mergeInto(LibraryManager.library, 
{

  YandexGame_GameReady: function () 
  {
    ysdk.features.LoadingAPI.ready();
  },

  SetLeaderboardScore: function (score) 
  {
    SetLeaderboardScore(score);
  }
});