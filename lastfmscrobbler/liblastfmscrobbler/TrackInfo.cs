namespace liblastfmscrobbler
{
  public class TrackInfo
  {
    public long pid { get; set; }
    public string title { get; set; }
    public string artist { get; set; }
    public string album { get; set; }
    public string album_artist { get; set; }
    public long date_played { get; set; }
    public long play_count_user { get; set; }
  }
}
