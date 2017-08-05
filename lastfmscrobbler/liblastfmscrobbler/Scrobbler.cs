using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace liblastfmscrobbler
{
  public class Scrobbler
  {
    string iPodControlPath = Path.Combine("iPod_Control", "iTunes", "iTunes Library.itlp");
    public IList<TrackInfo> GetData()
    {
      var path = Path.Combine(@"D:\", iPodControlPath);

      var dblibrary = new DbLibraryReader(path);
      var items = dblibrary.GetItemList();
      dblibrary.CloseConnection();

      var dbDynamic = new DbDynamicReader(path);
      var item_stats = dbDynamic.GetItemStatsList();
      dbDynamic.CloseConnection();

      var tracks = new List<TrackInfo>();

      foreach (var item in items)
      {
        var track = new TrackInfo()
        {
          pid = Convert.ToInt64(item["pid"]),
          title = Convert.ToString(item["title"]),
          artist = Convert.ToString(item["artist"]),
          album = Convert.ToString(item["album"]),
          album_artist = Convert.ToString(item["album_artist"]),
          date_played = 0,
          play_count_user = 0
        };
        var stat = item_stats.Where(s => Convert.ToInt64(s["item_pid"]) == track.pid).FirstOrDefault();
        if (stat != null)
        {
          track.date_played = Convert.ToInt64(stat["date_played"]);
          track.play_count_user = Convert.ToInt64(stat["play_count_user"]);
        }
        tracks.Add(track);
      }

      return tracks;
    }

  }
}
