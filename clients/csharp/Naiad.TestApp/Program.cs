using System;
using System.IO;
using Naiad.Libraries.Networking.Client;

class TestApp
{
    public static void Main()
    {
        string url = Environment.GetEnvironmentVariable("NAIAD_TEST_URL");
        string key = Environment.GetEnvironmentVariable("NAIAD_TEST_KEY");
        string secret = Environment.GetEnvironmentVariable("NAIAD_TEST_SECRET");

        NaiadClient client = new NaiadClient(
            url,
            key,
            secret);

        client.Login();

        using (Stream fs = File.Open(
                   "D:\\Data\\NaiadTestFiles\\testdata2.json",
                   FileMode.Open))
        {
            client.UploadData("testing/", "testdata2.json", fs);

            using (Stream ds = client.DownloadData("testing/testdata2.json"))
            {
                using (Stream dfs = File.Open(
                           "D:\\Data\\NaiadTestFiles\\testdata2_down.json", 
                           FileMode.CreateNew))
                {
                    ds.CopyTo(dfs);
                    dfs.Flush();
                    dfs.Close();
                }
            }
        }

        client.Logout();
    }
}
