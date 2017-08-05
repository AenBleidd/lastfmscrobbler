using liblastfmscrobbler;

namespace lastfmscrobbler
{
  class Program
  {
    static void Main(string[] args)
    {
      var c = new Scrobbler();
      var data = c.GetData();
      return;
    }
  }
}
