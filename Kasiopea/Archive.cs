using System.Text;

namespace Kasiopea // SOLVED
{
    public static class Kasiopea1
    {
        public static int NejkratsiVzdalenost(int sirka, int delka, int predni, int zadni)
        {
            int zleva = predni + delka + zadni;
            int zprava = (sirka - predni) + delka + (sirka - zadni);

            return zleva <= zprava ? zleva : zprava;
        }

        public static void PrectiVstup(TextReader vstup, TextWriter vystup)
        {
            int pocetProblemu = int.Parse(vstup.ReadLine()!);

            for (int i = 0; i < pocetProblemu; i++)
            {
                int sirka = int.Parse(vstup.ReadLine()!);
                int delka = int.Parse(vstup.ReadLine()!);
                int predni = int.Parse(vstup.ReadLine()!);
                int zadni = int.Parse(vstup.ReadLine()!);

                vystup.WriteLine(NejkratsiVzdalenost(sirka, delka, predni, zadni));
            }
        }

        public static void Main_()
        {
            using var vstup = new StreamReader(File.OpenRead("C:\\Users\\kocna\\Downloads\\A.txt"));
            using var vystup = new StreamWriter(File.OpenWrite("C:\\Users\\kocna\\Downloads\\A-vysledek.txt"));

            PrectiVstup(vstup, vystup);
        }
    }

    public static class Kasiopea2
    {
        public static int RecordsBroken(int[] data)
        {
            int lastMax = 0;

            int c = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > lastMax)
                {
                    lastMax = data[i];
                    c++;
                }
            }

            return c;
        }

        public static void PrectiVstup(TextReader vstup, TextWriter vystup)
        {
            int pocetProblemu = int.Parse(vstup.ReadLine()!);

            for (int i = 0; i < pocetProblemu; i++)
            {
                vstup.ReadLine();

                string line = vstup.ReadLine();
                string[] s = line.Split(' ');

                int[] a = new int[s.Length];

                for (int j = 0; j < a.Length; j++)
                {
                    a[j] = int.Parse(s[j]);
                }

                vystup.WriteLine(RecordsBroken(a));
            }
        }

        public static void Main_()
        {
            using var vstup = new StreamReader(File.OpenRead("C:\\Users\\kocna\\Downloads\\A.txt"));
            using var vystup = new StreamWriter(File.OpenWrite("C:\\Users\\kocna\\Downloads\\A-vysledek.txt"));

            PrectiVstup(vstup, vystup);
        }
    }

    public static class Kasiopea3
    {
        public static long DistanceTravelled(long[] data, long l)
        {
            recPos = 0;

            long distance = 0;

            List<long> d = data.ToList();

            Dictionary<long, int[]> hashMap = new Dictionary<long, int[]>(); // value, id positions

            for (int i = 0; i < data.Length; i++)
            {
                if (!hashMap.TryGetValue(data[i], out _))
                {
                    hashMap.Add(data[i], new int[2] { i, 0 });
                    continue;
                }

                hashMap[data[i]] = new int[2] { hashMap[data[i]][0], i };
            }

            Console.WriteLine("Hashmap created");

            Console.WriteLine($"For {l}, data: {data.Length}");

            for (int i = 0; i < l; i++)
            {
                distance += GetNext(d, hashMap);

                if (i == 10_000) Console.WriteLine("10 000");
                else if (i == 25_000) Console.WriteLine("25 000");
                else if (i == 50_000) Console.WriteLine("50 000");
                else if (i == 100_000) Console.WriteLine("100 000");
                else if (i == 150_000) Console.WriteLine("150 000");
            }

            Console.WriteLine($"--> {distance}");
            return distance;
        }

        private static int GetNext(List<long> data, Dictionary<long, int[]> hashmap)
        {
            for (int i = recPos; i < data.Count; i++)
            {
                if (data[i] == -1) continue;

                int[] positions = hashmap[data[i]];

                int nextpos = i == positions[0] ? positions[1] : positions[0];

                data[i] = -1;
                data[nextpos] = -1;

                int next = GetNextPosition(data, out int m);

                //Console.WriteLine($"{(j - i) + (Math.Abs(j - next))}, ({j} - {i}) + Abs({j} - {next})");
                return (nextpos - i) + (Math.Abs(nextpos - next) * m); // distance between Si & Sj

                /*for (int j = 0; j < data.Count; j++)
                {
                    if (i == j || data[j] == -1) continue;

                    if (data[i] == data[j])
                    {
                        data[i] = -1;
                        data[j] = -1;

                        int next = GetNextPosition(data, out int m);

                        //Console.WriteLine($"{(j - i) + (Math.Abs(j - next))}, ({j} - {i}) + Abs({j} - {next})");
                        return (j - i) + (Math.Abs(j - next) * m); // distance between Si & Sj
                    }
                }*/
            }

            throw new Exception();
        }

        private static int recPos = 0;

        private static int GetNextPosition(List<long> data, out int m)
        {
            m = 1;

            for (int i = recPos; i < data.Count; i++)
            {
                if (data[i] != -1)
                {
                    recPos = i;
                    return i;
                }
            }

            m = 0;
            return -1;
        }

        public static void PrectiVstup(TextReader vstup, TextWriter vystup)
        {
            int pocetProblemu = int.Parse(vstup.ReadLine()!);

            Console.WriteLine(pocetProblemu);

            for (int i = 0; i < pocetProblemu; i++)
            {
                long count = long.Parse(vstup.ReadLine());

                string line = vstup.ReadLine();
                string[] s = line.Split(' ');

                long[] a = new long[s.Length];

                for (int j = 0; j < a.Length; j++)
                {
                    a[j] = long.Parse(s[j]);
                }

                vystup.WriteLine(DistanceTravelled(a, count));
                Console.WriteLine("--------");
            }
        }

        public static void Main_()
        {
            using var vstup = new StreamReader(File.OpenRead("C:\\Users\\kocna\\Downloads\\A.txt"));
            using var vystup = new StreamWriter(File.OpenWrite("C:\\Users\\kocna\\Downloads\\A-vysledek.txt"));

            PrectiVstup(vstup, vystup);
        }
    }

    // SMS ANOTHER ATTEMPT
    public static class Kasiopea51
    {
        private class Word
        {
            public bool banned;

            public int startId;
            public int endId;

            public string content;

            public int Length => endId - startId + 1;

            public Word(int start, int end, string _content)
            {
                startId = start;
                endId = end;
                content = _content;
            }

            public override string ToString() => $"{content} ({startId} - {endId} ({Length}))";
        }

        private static bool GetMessage(string mssg, string[] dictionary, out string sentence)
        {
            string s = mssg;

            List<Word> words1 = new();

            bool[] bools = new bool[s.Length + 1];
            bools[^1] = true;

            DateTime prevTime = DateTime.Now;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (i != 0 && (s.Length - i) % 2500 == 0)
                {
                    Console.WriteLine($"{s.Length - i}/{s.Length} ({DateTime.Now} - est: {DateTime.Now + ((s.Length - 1 - (s.Length - i)) / 2500) * (DateTime.Now - prevTime)})");
                    prevTime = DateTime.Now;
                }

                foreach (string word in dictionary)
                {
                    string cWord = "";

                    if (i + word.Length <= s.Length && (cWord = s.Substring(i, word.Length)) == word)
                    {
                        bools[i] = bools[i + word.Length];
                        if (bools[i])
                        {
                            words1.Add(new Word(i, i + word.Length - 1, cWord));
                            break;
                        }
                    }
                }
            }

            if (!bools[0])
            {
                sentence = "";
                return false;
            }

            Console.WriteLine($"WORDS GENERATED ({words1.Count})");

            List<Word> fWords = new List<Word>();

            int nextId = 0;

            while (true)
            {
                Word cur = GetWordWithStart(words1, nextId);

                if (cur == null)
                {
                    fWords[^1].banned = true;
                    nextId = fWords[^1].startId - 1;

                    fWords.RemoveAt(fWords.Count - 1);
                }
                else
                {
                    fWords.Add(cur);
                    nextId = cur.endId + 1;
                }

                if (nextId == mssg.Length) break;
            }

            sentence = GetSentence(fWords);

            return bools[0];
        }

        private static string GetSentence(List<Word> words)
        {
            StringBuilder builder = new();

            for (int i = 0; i < words.Count; i++)
            {
                builder.Append(words[i].content);
                if (i != words.Count - 1) builder.Append(" ");
            }

            return builder.ToString();
        }

        private static Word? GetWordWithStart(List<Word> words, int start)
        {
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].banned) continue;

                if (words[i].startId == start) return words[i];
            }

            return null;
        }

        public static void PrectiVstup(TextReader vstup, TextWriter vystup)
        {
            int pocetProblemu = int.Parse(vstup.ReadLine()!);

            Console.WriteLine(pocetProblemu);

            Console.WriteLine($"problemy: {pocetProblemu}");

            for (int i = 0; i < pocetProblemu; i++)
            {
                int count = int.Parse(vstup.ReadLine());

                string message = vstup.ReadLine();

                Console.WriteLine($"({i + 1}/{pocetProblemu})vstup: {message.Length} slovnik: {count}");

                string[] dictionary = new string[count];

                for (int j = 0; j < count; j++)
                {
                    dictionary[j] = vstup.ReadLine();
                }

                bool b = GetMessage(message, dictionary, out string s);

                Console.WriteLine($"{b} ({s})");

                if (b)
                {
                    vystup.WriteLine("ANO");
                    vystup.WriteLine(s);
                }
                else vystup.WriteLine("NE");
            }
        }

        public static void Main_()
        {
            using var vstup = new StreamReader(File.OpenRead("C:\\Users\\kocna\\Downloads\\A.txt"));
            using var vystup = new StreamWriter(File.OpenWrite("C:\\Users\\kocna\\Downloads\\A-vysledek.txt"));

            PrectiVstup(vstup, vystup);
        }
    }
}

namespace sdkj // SOLVED PARTIALLY OR NOT SOLVED AT ALL
{
    // FCKING VLAKY
    public static class Kasiopea9
    {
        private class City
        {
            public int id;

            public List<City> connectedTo = new List<City>();
            public List<long> capacity = new List<long>();

            public void AddConnection(City _city, long _capacity)
            {
                connectedTo.Add(_city);
                capacity.Add(_capacity);
            }

            public override string ToString() => $"City {id}";
        }

        private static City start;
        private static City end;

        private static long idealPathMaxCapacity;

        private static long GetMaxCapacity(City[] cities, City _start, City _end)
        {
            Console.WriteLine($"--> {cities.Length}");

            start = _start;
            end = _end;

            idealPathMaxCapacity = long.MaxValue;

            Console.WriteLine($"Start: {start}, End: {end}");

            List<City> idealPath = GetPath((null, null), false, false, out long maxCapacity);

            if (maxCapacity == 0) return 0;

            idealPathMaxCapacity = maxCapacity;

            for (int i = 0; i < idealPath.Count - 1; i++)
            {
                //Console.WriteLine($"------ BANNING {idealPath[i]}, {idealPath[i + 1]}");

                start = idealPath[i];
                end = idealPath[i + 1];

                GetPath((idealPath[i], idealPath[i + 1]), true, true, out long capacity);

                //Console.WriteLine($"Capacity of path is: {capacity}");

                if (capacity == 0) return 0;

                if (capacity < maxCapacity)
                {
                    maxCapacity = capacity;
                }
            }

            return maxCapacity;

            // FOR EACH CITY IN PATH 
            // -- RECALCULATE PATH
        }

        /// <param name="bannedPath"> (from, to) </param>
        private static List<City> GetPath((City, City) bannedPath, bool capOnly, bool limitedAcc, out long maxCapacity)
        {
            Dictionary<City, (long, City)> datasheet = GetDataSheet(bannedPath, limitedAcc);

            //LogDataSheet(datasheet);

            List<City> path = new List<City>();

            maxCapacity = long.MaxValue;

            if (capOnly)
            {
                maxCapacity = datasheet[end].Item1;
                return null;
            }

            City c1 = end;
            while (c1 != start)
            {
                (long, City) data = (-1, null);

                try
                {
                    data = datasheet[c1];
                }
                catch
                {
                    maxCapacity = 0;
                    return null;
                }

                path.Insert(0, c1);

                for (int i = 0; i < c1.connectedTo.Count; i++)
                {
                    if (c1.connectedTo[i] == data.Item2)
                    {
                        long capacity = IsPath(c1, c1.connectedTo[i], bannedPath.Item1, bannedPath.Item2) ? 0 : c1.capacity[i];

                        //Console.WriteLine($"building path {c1}, {c1.connectedTo[i]} |{bannedPath}| - {capacity}");

                        if (capacity < maxCapacity) maxCapacity = capacity;
                    }
                }

                c1 = data.Item2;
            }
            path.Insert(0, start);

            return path;
        }

        private static void LogDataSheet(Dictionary<City, (long, City)> datasheet)
        {
            foreach (var v in datasheet) Console.WriteLine($"{v.Key.id} | {v.Value.Item1} | {v.Value.Item2.id}");
            Console.WriteLine("--------------");
        }

        private static Dictionary<City, (long, City)> GetDataSheet((City, City) bannedPath, bool limitedAccuracy)
        {
            usedCities.Clear();

            Dictionary<City, (long, City)> datasheet = new Dictionary<City, (long, City)>(); // city, max cap (from start), prev city

            List<City> c = AddToDataSheet1(datasheet, start, bannedPath, limitedAccuracy, out bool pStop);

            if (pStop) return datasheet;

            Stack<List<City>> stack = new Stack<List<City>>();
            stack.Push(c);

            while (stack.Count != 0)
            {
                List<City> l = stack.Pop();

                for (int i = 0; i < l.Count; i++)
                {
                    c = AddToDataSheet1(datasheet, l[i], bannedPath, limitedAccuracy, out pStop);

                    if (pStop)
                    {
                        Console.WriteLine("LimitedAcc applied");
                        return datasheet;
                    }

                    if (c.Count != 0) stack.Push(c);
                }
            }

            return datasheet;
        }
        private static HashSet<City> usedCities = new HashSet<City>();

        private static List<City> AddToDataSheet1(Dictionary<City, (long, City)> datasheet, City city, (City, City) bannedPath, bool limitedAccuracy, out bool prevStop)
        {
            prevStop = false;

            for (int i = 0; i < city.connectedTo.Count; i++)
            {
                City targetCity = city.connectedTo[i];

                //Console.WriteLine($"- Adding for {city.id} ({city.connectedTo.Count}) t: {targetCity}");

                if (usedCities.TryGetValue(targetCity, out _)) continue;
                usedCities.Add(city);

                AddDataToSheet1(datasheet, city, targetCity, bannedPath, i);

                if (limitedAccuracy && targetCity == end && datasheet[targetCity].Item1 >= idealPathMaxCapacity)
                {
                    prevStop = true;
                    return null;
                }

                //if (targetCity != end) AddToDataSheet1(datasheet, targetCity, bannedPath);
            }

            List<City> citiesToSearch = new List<City>();

            for (int i = 0; i < city.connectedTo.Count; i++)
            {
                City targetCity = city.connectedTo[i];
                if (targetCity != end && !usedCities.TryGetValue(targetCity, out _))
                {
                    citiesToSearch.Add(targetCity);
                    //AddToDataSheet1(datasheet, targetCity, bannedPath);
                }
            }

            return citiesToSearch;
        }

        private static void AddDataToSheet1(Dictionary<City, (long, City)> datasheet, City city, City targetCity, (City, City) bannedPath, int i)
        {
            long capacity = IsPath(city, targetCity, bannedPath.Item1, bannedPath.Item2) ? 0 : city.capacity[i];
            capacity = datasheet.TryGetValue(city, out (long, City) v) ? Math.Min(v.Item1, capacity) : capacity;

            if (datasheet.TryGetValue(targetCity, out (long, City) value)) // if city is already there, try to overwrite capacity
            {
                //Console.WriteLine($" - - {targetCity.id} already exists, ?({value.Item1} < {capacity})");

                if (value.Item1 < capacity)
                {
                    datasheet[targetCity] = (capacity, city);
                }
            }
            else
            {
                //Console.WriteLine($" - - adding {targetCity} (capacity: {capacity})");

                datasheet.Add(targetCity, (capacity, city));
            }
        }

        private static bool IsPath(City a1, City a2, City b1, City b2)
        {
            return (a1 == b1 && a2 == b2) || (a1 == b2 && a2 == b1);
        }

        public static void PrectiVstup(TextReader vstup, TextWriter vystup)
        {
            int pocetProblemu = int.Parse(vstup.ReadLine()!);

            Console.WriteLine(pocetProblemu);

            Console.WriteLine($"problemy: {pocetProblemu}");

            for (int i = 0; i < pocetProblemu; i++)
            {
                long[] v = GetLineData(vstup.ReadLine());

                int citiesCount = (int)v[0];
                int trailsCount = (int)v[1];
                int eventsCount = (int)v[2];

                City[] cities = new City[citiesCount];

                for (int j = 0; j < cities.Length; j++)
                {
                    cities[j] = new City();
                    cities[j].id = j + 1;
                }

                for (int j = 0; j < trailsCount; j++)
                {
                    long[] v1 = GetLineData(vstup.ReadLine());

                    int x = (int)v1[0] - 1;
                    int y = (int)v1[1] - 1;

                    long capacity = v1[2];

                    cities[x].AddConnection(cities[y], capacity);
                    cities[y].AddConnection(cities[x], capacity);
                }

                List<(City, City)> events = new List<(City, City)>(); // city 1, city 2

                for (int j = 0; j < eventsCount; j++)
                {
                    long[] v1 = GetLineData(vstup.ReadLine());

                    events.Add((cities[v1[0] - 1], cities[v1[1] - 1]));
                }

                Console.WriteLine($"{i + 1}/{pocetProblemu} --- Events: {events.Count}");

                for (int j = 0; j < events.Count; j++)
                {
                    long maxC = GetMaxCapacity(cities, events[j].Item1, events[j].Item2);

                    Console.WriteLine($"Max capacity: {maxC}");

                    vystup.WriteLine(maxC);
                    //Console.WriteLine("---------------------------------------------- ---------------------------------------------- \n\n");
                }
            }
        }

        private static long[] GetLineData(string line)
        {
            string[] parts = line.Split(' ');
            long[] a = new long[parts.Length];

            for (long i = 0; i < parts.Length; i++) a[i] = long.Parse(parts[i]);

            return a;
        }

        public static void Main_()
        {
            using var vstup = new StreamReader(File.OpenRead("C:\\Users\\kocna\\Downloads\\A.txt"));
            using var vystup = new StreamWriter(File.OpenWrite("C:\\Users\\kocna\\Downloads\\A-vysledek.txt"));

            PrectiVstup(vstup, vystup);
        }
    }

    // ROZHLEDNY
    public static class Kasiopea7
    {
        private struct Lookout
        {
            public long beauty;

            public long distanceToNext;
            public long routeBeauty;

            public Lookout(long b, long rb, long d)
            {
                beauty = b;
                distanceToNext = d;
                routeBeauty = rb;
            }
        }

        private static (long, long) GetRoute(Lookout[] lookouts, long maxDist)
        {
            (long, long) v = (0, 0);

            long maxBeauty = 0;

            for (int i = 0; i < lookouts.Length - 1; i++)
            {
                if (i % 1000 == 0) Console.WriteLine(i);

                long dist = 0;
                long beauty = 0;

                int j = i; // it should start on 'i' & 'i + 1'

                do
                {
                    j++;

                    Lookout nextLookout = lookouts[j];
                    Lookout prevLookout = lookouts[j - 1];

                    dist += prevLookout.distanceToNext;
                    beauty += prevLookout.routeBeauty;

                    long totBeauty = beauty + lookouts[i].beauty + nextLookout.beauty;

                    if (totBeauty > maxBeauty)
                    {
                        maxBeauty = totBeauty;
                        v = (i + 1, j + 1);
                    }

                } while (j < lookouts.Length - 1 && dist <= maxDist - lookouts[j].distanceToNext);
            }

            return v;
        }

        public static void PrectiVstup(TextReader vstup, TextWriter vystup)
        {
            int pocetProblemu = int.Parse(vstup.ReadLine()!);

            Console.WriteLine(pocetProblemu);

            Console.WriteLine($"problemy: {pocetProblemu}");

            for (int i = 0; i < pocetProblemu; i++)
            {
                long[] v = GetLineData(vstup.ReadLine());

                long count = v[0];
                long maxDist = v[1];

                Console.WriteLine($"New: count: {count}, maxDist: {maxDist} ");

                long[] beauties = GetLineData(vstup.ReadLine());

                Lookout[] lookouts = new Lookout[count];

                for (int j = 0; j < count; j++)
                {
                    //Console.WriteLine($"Creating new lookout");

                    long[] v1 = j != count - 1 ? GetLineData(vstup.ReadLine()) : new long[2] { 0, 0 };

                    lookouts[j] = new Lookout(beauties[j], v1[0], v1[1]);
                }

                Console.WriteLine($"Lookouts generated ({lookouts.Length})");

                Console.WriteLine($"---------- ---------- GET ROUTE {i + 1}/{pocetProblemu} ---------- ---------- {DateTime.Now}");

                (long, long) output = GetRoute(lookouts, maxDist);

                Console.WriteLine($"{output}");

                vystup.WriteLine($"{output.Item1} {output.Item2}");

                Console.WriteLine($"DONE {DateTime.Now}");
            }
        }

        private static long[] GetLineData(string line)
        {
            string[] parts = line.Split(' ');
            long[] a = new long[parts.Length];

            for (int i = 0; i < parts.Length; i++) a[i] = long.Parse(parts[i]);

            return a;
        }

        public static void Main_()
        {
            using var vstup = new StreamReader(File.OpenRead("C:\\Users\\kocna\\Downloads\\A.txt"));
            using var vystup = new StreamWriter(File.OpenWrite("C:\\Users\\kocna\\Downloads\\A-vysledek.txt"));

            PrectiVstup(vstup, vystup);
        }
    }

    // PAT
    public static class Kasiopea4
    {
        private class House
        {
            private int id;

            public House(int _id) { id = _id; }

            public List<Path> connections = new List<Path>();

            public List<long> preDistances = new List<long>();

            public House GetConnection(int id) => connections[id].GetConnection(this);

            public override string ToString() => $"House {id}";
        }

        private class Path
        {
            public House h1;
            public House h2;
            public long distance;

            public House GetConnection(House source) => source == h1 ? h2 : h1;
        }

        private static long GetDistance(House[] houses)
        {
            if (houses.Length == 1) return 0;

            House prevStart = null;
            House start = null;
            long furthest = long.MinValue;

            for (int i = 0; i < houses.Length; i++)
            {
                if (houses[i].connections.Count != 1) continue;

                House c = houses[i];
                long distance = 0;

                House prev = null;

                while (c.connections.Count <= 2) // the next nodes will have 2 paths
                {
                    if (c != houses[i] && c.connections.Count == 1) break;

                    MoveStraightPath(ref c, ref prev, out long dist);

                    distance += dist;
                }

                if (distance > furthest)
                {
                    furthest = distance;
                    //start = houses[i];
                    prevStart = prev;
                    start = c;
                }

                // GET FURTHEST POINT FROM NEXT INTERSECTION
                // START ON THIS POINT
            }

            // CONTINUE ON THIS PATH 
            // IF INTERCECTION IS FOUND --> SEARCH THEM (ALL OF THEM AT THE SAME TIME) --> FIND THE SMALLEST ONE
            // FOR THE SHORTEST ONE: MULTIPLY ITS TOTAL DISTANCE BY 2 AND CONTINUE UNTIL THERE IS ONLY ONE PATH LEFT (THE LONGEST ONE, THEN CONTINUE ON IT, UNTIL THERE IS NO OTHER HOUSE LEFT)

            long minDist = long.MaxValue;

            /*for (int i = 0; i < houses.Length; i++)
            {
                if (houses[i].connections.Count != 1) continue;

                if (enableLogs) Console.WriteLine($"Calculating for {houses[i]}");
                long dist = CalculateDistance(houses[i].GetConnection(0), houses[i], houses[i].connections[0].distance);
                if (enableLogs) Console.WriteLine($"result: {dist}, \n\n\n");

                if (dist < minDist) minDist = dist;
            }*/

            long cur = CalculateDistance(start, prevStart, furthest);

            Console.WriteLine($"{cur == minDist}, {cur}, {minDist}");

            return cur;
        }

        private static long CalculateDistance(House start, House prevStart, long furthest)
        {
            long totalDistance = furthest;

            if (enableLogs) Console.WriteLine($"Starting on {start}, {prevStart} with total distance of {totalDistance}");

            House prevHouse = prevStart;
            House currentHouse = start;

            while (currentHouse.connections.Count != 1)
            {
                bool intersection = currentHouse.connections.Count > 2;

                if (intersection)
                {
                    int direction = HandleIntersection(ref currentHouse, prevHouse, out long distanceToAdd);

                    totalDistance += distanceToAdd;

                    if (enableLogs) Console.WriteLine($"Intersection solved! next: {currentHouse.GetConnection(direction)}, distToAdd: {distanceToAdd}, totalDist: {totalDistance} \n");

                    Path p = currentHouse.connections[direction];
                    totalDistance += p.distance;

                    prevHouse = currentHouse;
                    currentHouse = p.GetConnection(currentHouse);

                    if (enableLogs) Console.WriteLine($"cur: {currentHouse}, tDist: {totalDistance}");

                    continue;
                }

                MoveStraightPath(ref currentHouse, ref prevHouse, out long distance);
                totalDistance += distance;

                if (enableLogs) Console.WriteLine($"cur: {currentHouse}, tDist: {totalDistance}");
            }

            return totalDistance;
        }

        private static readonly bool enableLogs = false;

        // RETURN WHICH WAY TO CONTINUE & WHAT DISTANCE TO ADD
        private static int HandleIntersection(ref House currentHouse, House prevHouse, out long distanceToAdd, string pref = "")
        {
            if (enableLogs) Console.WriteLine($"{pref}Solving intersection: {currentHouse}, prev: {prevHouse}");

            int arrLength = currentHouse.connections.Count;

            long[] distances = new long[arrLength];

            HashSet<House> usedHouses = new HashSet<House> { currentHouse };

            Queue<(House, int)> housesToCover = new Queue<(House, int)>(); // KEEP REFFERENCE OF BRANCH ID

            if (currentHouse.preDistances.Count == 0)
            {
                for (int j = 0; j < currentHouse.connections.Count; j++)
                {
                    House h = currentHouse.GetConnection(j);
                    if (h == prevHouse) continue;

                    if (enableLogs) Console.WriteLine($"{pref}- - Direction {j}: {h}");

                    housesToCover.Enqueue((h, j));
                    distances[j] += currentHouse.connections[j].distance;

                    usedHouses.Add(h);
                }
            }
            else
            {
                for (int j = 0; j < currentHouse.connections.Count; j++)
                {
                    House h = currentHouse.GetConnection(j);
                    if (h == prevHouse) continue;

                    if (enableLogs) Console.WriteLine($"{pref}- - Direction {j}: {h}");

                    //housesToCover.Enqueue((h, j));
                    distances[j] += currentHouse.preDistances[j];

                    usedHouses.Add(h);
                }
            }

            var intersectionsToHandle = new List<(House, House, int)>();

            while (housesToCover.Count != 0)
            {
                (House, int) val = housesToCover.Dequeue();

                House curHouse = val.Item1; // current house, distance has already been added

                if (enableLogs) Console.WriteLine($"{pref}- - - - Handling house {curHouse} (path {val.Item2})");

                if (curHouse.connections.Count == 1) continue;

                if (curHouse.connections.Count > 2)
                {
                    House prev = null;

                    for (int i = 0; i < curHouse.connections.Count; i++)
                    {
                        if (usedHouses.TryGetValue(curHouse.GetConnection(i), out _)) prev = curHouse.GetConnection(i);
                    }

                    intersectionsToHandle.Add((curHouse, prev, val.Item2));

                    /*int c = HandleIntersection(ref curHouse, prev, out long dToAdd);

                    housesToCover.Enqueue((curHouse.GetConnection(c), val.Item2));
                    distances[val.Item2] += dToAdd + curHouse.connections[c].distance;*/

                    continue;
                }

                for (int i = 0; i < curHouse.connections.Count; i++)
                {
                    House nextHouse = curHouse.GetConnection(i);

                    if (usedHouses.TryGetValue(nextHouse, out _)) continue;
                    usedHouses.Add(nextHouse);

                    housesToCover.Enqueue((nextHouse, val.Item2));
                    distances[val.Item2] += curHouse.connections[i].distance;

                    if (enableLogs) Console.WriteLine($"{pref}- - - - - - Adding house {nextHouse} ({curHouse.connections[i].distance})");
                }
            }

            for (int i = 0; i < intersectionsToHandle.Count; i++)
            {
                var d = intersectionsToHandle[i];

                House curHouse = d.Item1;
                House prev = d.Item2;
                int id = d.Item3;

                int c = HandleIntersection(ref curHouse, prev, out long dToAdd, pref + " - -");

                //housesToCover.Enqueue((curHouse.GetConnection(c), id));
                distances[id] += dToAdd + curHouse.connections[c].distance;

                prev = curHouse;
                curHouse = curHouse.GetConnection(c);

                distances[id] += CalculateDistance(curHouse, prev, 0);
            }

            if (housesToCover.Count != 0) throw new Exception();

            int direction = 0;
            long maxDist = long.MinValue;

            for (int i = 0; i < arrLength; i++)
            {
                if (enableLogs) Console.WriteLine($"{pref}- - Distance {i}: {distances[i]}");

                if (distances[i] > maxDist)
                {
                    direction = i;
                    maxDist = distances[i];
                }
            }

            for (int i = 0; i < distances.Length; i++)
            {
                currentHouse.preDistances.Add(distances[i]);
            }

            distanceToAdd = 0;

            for (int i = 0; i < arrLength; i++)
            {
                if (direction == i) continue;

                distanceToAdd += 2 * distances[i];
            }

            return direction;
            // GET RESULTS OF THIS
        }

        private static void MoveStraightPath(ref House cHouse, ref House prevHouse, out long distance)
        {
            House h = cHouse.GetConnection(0);
            int cId = h == prevHouse ? 1 : 0;

            prevHouse = cHouse;

            distance = cHouse.connections[cId].distance;
            cHouse = cHouse.GetConnection(cId);
        }

        public static void PrectiVstup(TextReader input, TextWriter output)
        {
            int pocetProblemu = int.Parse(input.ReadLine()!);

            Console.WriteLine(pocetProblemu);

            for (int i = 0; i < pocetProblemu; i++)
            {
                long count = long.Parse(input.ReadLine());

                House[] houses = new House[count];

                for (int j = 0; j < count; j++) houses[j] = new House(j + 1);

                for (int j = 0; j < count - 1; j++)
                {
                    long[] vals = GetLineData(input.ReadLine());

                    House h1 = houses[(int)vals[0] - 1];
                    House h2 = houses[(int)vals[1] - 1];

                    Path p = new Path() { h1 = h1, h2 = h2, distance = vals[2] };

                    h1.connections.Add(p);
                    h2.connections.Add(p);
                }

                if (enableLogs) LogConnections(houses);

                Console.WriteLine($"------- problem {i + 1}/{pocetProblemu}");
                Console.WriteLine($"Houses: {houses.Length}");
                long v = GetDistance(houses);

                output.WriteLine(v);
                Console.WriteLine($"min distance = {v}");

                Console.WriteLine("--------");
            }
        }

        private static void LogConnections(House[] houses)
        {
            for (int j = 0; j < houses.Length; j++)
            {
                Console.WriteLine($"\n{houses[j]} ");

                for (int z = 0; z < houses[j].connections.Count; z++)
                {
                    Path path = houses[j].connections[z];
                    Console.Write($" - {path.GetConnection(houses[j])} ({path.distance})");
                }
            }
            Console.WriteLine("\n--------------------\n\n");
        }

        private static long[] GetLineData(string line)
        {
            string[] parts = line.Split(' ');
            long[] a = new long[parts.Length];

            for (long i = 0; i < parts.Length; i++) a[i] = long.Parse(parts[i]);

            return a;
        }

        public static void Main_()
        {
            using var vstup = new StreamReader(File.OpenRead("C:\\Users\\kocna\\Downloads\\A.txt"));
            using var vystup = new StreamWriter(File.OpenWrite("C:\\Users\\kocna\\Downloads\\A-vysledek.txt"));

            PrectiVstup(vstup, vystup);
        }
    }
}

namespace AtThisPointIDK
{
    // SMS - Bruteforce (it does not even work anymore)
    public static class Kasiopea5
    {
        private static readonly bool enableConsoleLogs = false;

        private static bool GetMessage(string message, string[] o_dictionary, out string s)
        {
            couldExist.Clear();

            s = "";

            SortedSet<string> dict = new SortedSet<string>(); // word

            Dictionary<char, int> map = new Dictionary<char, int>();

            for (int i = 0; i < o_dictionary.Length; i++)
            {
                dict.Add(o_dictionary[i]);
            }

            string[] arr = dict.ToArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (!map.TryGetValue(arr[i][0], out _)) map.Add(arr[i][0], i);
            }

            List<string> words = new List<string>();

            Dictionary<string, List<(int, string)>> bannedWords = new Dictionary<string, List<(int, string)>>(); // word, position

            string cWord = "";

            for (int i = 0; i <= message.Length; i++)
            {
                if (i % 100 == 0) Console.WriteLine($"{i}/{message.Length}");

                bool last = false;

                if (i != message.Length)
                {
                    if (enableConsoleLogs) Console.WriteLine($"Adding {message[i]} ({i})");
                    cWord += message[i];
                }
                else last = true;

                //string recWord = words.Count == 0 ? "" : words[^1];

                bool isBanned = bannedWords.TryGetValue(cWord, out List<(int, string)> pos) && pos.Contains((words.Count, words.Count == 0 ? "" : words[^1]));

                if (dict.TryGetValue(cWord, out _) && !isBanned) // word is in dictionary & is not banned
                {
                    if (enableConsoleLogs) Console.WriteLine($"Adding word into list ({cWord})");

                    if (i == message.Length) break;

                    words.Add(cWord);
                    cWord = "";

                    continue;
                }
                else if (!WordCouldExist(cWord, dict, map) || (last && cWord.Length != 0)) // word is not in dictionary & can't be
                {
                    if (enableConsoleLogs) Console.WriteLine($"Word can't exist - clearing ({cWord}) (last: {last && cWord.Length != 0} ({i}))");

                    if (words.Count == 0) return false;

                    string recWord = words.Count <= 1 ? "" : words[^2];

                    (int, string) vToAdd = (words.Count - 1, recWord);

                    if (enableConsoleLogs) Console.WriteLine($"Banned for: {vToAdd}");

                    if (bannedWords.TryGetValue(words[^1], out _)) bannedWords[words[^1]].Add(vToAdd);
                    else bannedWords.Add(words[^1], new List<(int, string)>() { vToAdd });

                    //cWord = message[0].ToString();
                    //Console.WriteLine($"cur: {i}");
                    i -= cWord.Length + words[^1].Length;
                    if (last && cWord.Length != 0) i--;

                    cWord = "";
                    //Console.WriteLine($"continue: {i}");

                    words.RemoveAt(words.Count - 1);

                    continue;
                }
            }

            for (int i = 0; i < words.Count; i++)
            {
                if (i == words.Count - 1) s += words[i];
                else s += $"{words[i]} ";
            }

            return true;
        }

        private static HashSet<string> couldExist = new HashSet<string>();

        private static bool WordCouldExist(string word, SortedSet<string> dict, Dictionary<char, int> map)
        {
            if (word == "")
            {
                return false;
                throw new Exception();
            }

            if (couldExist.TryGetValue(word, out _)) return true;

            string[] arr = dict.ToArray();

            int start = map.TryGetValue(word[0], out int v) ? v : 0;

            for (int i = start; i < arr.Length; i++)
            {
                if (arr[i].StartsWith(word))
                {
                    couldExist.Add(word);
                    return true;
                }
            }

            return false; // Element not found

            /*foreach (var v in dict)
            {
                if (v.StartsWith(word)) return true;
            }*/

            return false;
        }

        public static void PrectiVstup(TextReader vstup, TextWriter vystup)
        {
            int pocetProblemu = int.Parse(vstup.ReadLine()!);

            Console.WriteLine(pocetProblemu);

            Console.WriteLine($"problemy: {pocetProblemu}");

            for (int i = 0; i < pocetProblemu; i++)
            {
                int count = int.Parse(vstup.ReadLine());

                string message = vstup.ReadLine();

                Console.WriteLine($"({i + 1}/{pocetProblemu})vstup: {message.Length} slovnik: {count}");

                string[] dictionary = new string[count];

                for (int j = 0; j < count; j++)
                {
                    dictionary[j] = vstup.ReadLine();
                }

                bool b = GetMessage(message, dictionary, out string s);

                Console.WriteLine($"{b} ({s})");

                if (b)
                {
                    vystup.WriteLine("ANO");
                    vystup.WriteLine(s);
                }
                else vystup.WriteLine("NE");
            }
        }

        public static void Main_()
        {
            using var vstup = new StreamReader(File.OpenRead("C:\\Users\\kocna\\Downloads\\A.txt"));
            using var vystup = new StreamWriter(File.OpenWrite("C:\\Users\\kocna\\Downloads\\A-vysledek.txt"));

            PrectiVstup(vstup, vystup);
        }
    }
}