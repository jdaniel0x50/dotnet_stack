
using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace _06_music_linq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            IEnumerable<Artist> allArtists =    from artist in Artists
                                                select artist;
            IEnumerable<Group> allGroups =  from group_g in Groups select group_g;

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            Console.WriteLine("Part One: Name of the artist from Mount Vernon");
            IEnumerable<Artist> foundArtists = Artists.Where(artist => artist.Hometown == "Mount Vernon");
            foreach (Artist artist in foundArtists)
            {
                Console.WriteLine("Artist Name = {0}", artist.ArtistName);
                Console.WriteLine("Artist Real Name = {0}", artist.RealName);
                Console.WriteLine("Artist Age = {0}", artist.Age);
                Console.WriteLine("Hometown : " + artist.Hometown);
            }

            //Who is the youngest artist in our collection of artists?
            Console.WriteLine("");
            Console.WriteLine("Part Two: Youngest Artist");
            int minAge = allArtists.Min(artist => artist.Age);
            foundArtists = allArtists.Where(artist => artist.Age == minAge);
            foreach (Artist artist in foundArtists)
            {
                Console.WriteLine("Artist Name = {0}", artist.ArtistName);
                Console.WriteLine("Artist Real Name = {0}", artist.RealName);
                Console.WriteLine("Artist Age = {0}", artist.Age);
            }

            //Display all artists with 'William' somewhere in their real name
            Console.WriteLine("");
            Console.WriteLine("Part Three: Artists with 'William' in real name");
            String searchString = "William";
            foundArtists = allArtists.Where(artist => artist.RealName.IndexOf(searchString) > 0);
            foreach (Artist artist in foundArtists)
            {
                Console.WriteLine("Artist Name (and Real Name) = {0} ({1}), age {2}", artist.ArtistName, artist.RealName, artist.Age);
            }

            //Display the 3 oldest artist from Atlanta
            Console.WriteLine("");
            Console.WriteLine("Part Four: Three Oldest Artists from Atlanta");
            allArtists =    from artist in allArtists
                            orderby artist.Age descending
                            select artist;
            searchString = "Atlanta";
            foundArtists = allArtists.Where(artist => artist.Hometown == searchString);
            foundArtists = foundArtists.Take(3);
            foreach (Artist artist in foundArtists)
            {
                Console.WriteLine("Artist Name (and Real Name) = {0} ({1}), age {2}", artist.ArtistName, artist.RealName, artist.Age);
            }

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            Console.WriteLine("");
            Console.WriteLine("Part Five: Group Name of all Groups with artists not from New York City");
            searchString = "New York City";
            foundArtists = allArtists.Where(artist => artist.Hometown != searchString);
            var query = (
                        from artist in foundArtists
                        join group_g in allGroups
                        on artist.GroupId
                        equals group_g.Id
                        orderby group_g.GroupName
                        select new { group_name = group_g.GroupName }
            ).Distinct();
            foreach (var query_result in query)
            {
                Console.WriteLine(query_result.group_name);
            }

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            Console.WriteLine("");
            Console.WriteLine("Part Six: All Artists from Group Wu-Tang Clan");
            searchString = "Wu-Tang Clan";
            var query2 = (
                from artist in Artists
                join group_g in Groups
                on artist.GroupId
                equals group_g.Id
                where group_g.GroupName == searchString
                orderby artist.ArtistName
                select new {Artist = artist, Group = group_g}
            );
            foreach (var artist in query2)
            {
                Console.WriteLine("Artist Name (and Real Name) = {0} ({1}), age {2} / Group: {3}", artist.Artist.ArtistName, artist.Artist.RealName, artist.Artist.Age, artist.Group.GroupName);
            }
        }
    }
}

