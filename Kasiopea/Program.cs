// SEMAFOR
public static class Kasiopea6
{
    private abstract class Instruction
    {
        public abstract long Duration { get; }
        public long TotalTime { get => totalTime; } // total duration withing current loop 

        private long totalTime;

        protected Instruction(long tTime) { totalTime = tTime; }
    }

    private class Repeat : Instruction
    {
        public List<Instruction> instructions;

        public override long Duration { get => duration * repCount; }
        public long RepDuration => duration;

        private long duration;
        public long repCount;

        public Repeat(long _repCount, long totTime, List<Instruction> _instructions) : base(totTime)
        {
            repCount = _repCount;

            instructions = _instructions;

            for (int i = 0; i < instructions.Count; i++)
            {
                duration += instructions[i].Duration;
            }
        }

        public override string ToString() { return "Repeat"; }
    }

    private class RepeatBuilder
    {
        public List<Instruction> instructions = new List<Instruction>();
        private long repetitions;
        private long totTime;

        public RepeatBuilder(long _repetitions, long _totTime) { repetitions = _repetitions; totTime = _totTime; }

        public void Add(Instruction inc) => instructions.Add(inc);

        public Repeat ToRepeat() => new Repeat(repetitions, totTime, instructions);
    }

    private class Show : Instruction
    {
        public override long Duration { get => duration; }
        private long duration;

        public char color;

        public Show(long _duration, long totTime, char _color) : base(totTime)
        {
            duration = _duration;
            color = _color;
        }

        public override string ToString() => $"SHOW {duration}s {color}";
    }

    // --------------- --------------- --------------- ---------------
    private static char GetColor(List<Instruction> instructions, long targetTime)
    {
        Instruction curIns = FindInstruction(instructions, targetTime);

        while (curIns is Repeat r)
        {
            long nTargetTime = (targetTime - r.TotalTime) % r.RepDuration;

            curIns = FindInstruction(r.instructions, nTargetTime);
            if (enableLogs) Console.WriteLine($"Get color on {targetTime}: {nTargetTime} --> {curIns}");

            targetTime = nTargetTime;
        }

        return (curIns as Show).color;
    }

    private static Instruction FindInstruction(List<Instruction> instructions, long targetTime)
    {
        int left = 0, right = instructions.Count - 1;

        while (right >= left)
        {
            int mid = (left + right) / 2;

            Instruction curIns = instructions[mid];

            if (curIns.TotalTime <= targetTime && curIns.TotalTime + curIns.Duration > targetTime) return curIns;
            else if (curIns.TotalTime > targetTime) right = mid - 1;
            else left = mid + 1;
        }

        throw new Exception("Not found");
    }

    private static bool enableLogs = false;

    public static void PrectiVstup(TextReader input, TextWriter output)
    {
        int pocetProblemu = int.Parse(input.ReadLine()!);

        Console.WriteLine(pocetProblemu);

        Console.WriteLine($"problemy: {pocetProblemu}");

        for (int i = 0; i < pocetProblemu; i++)
        {
            Console.WriteLine($"{i + 1}/{pocetProblemu}");

            if (input.ReadLine() != "BEGIN") throw new Exception("Invalid program start");

            List<Instruction> instructions = GetInstructions(input, out int rLength);

            int commandLength = int.Parse(input.ReadLine());
            long[] commands = new long[commandLength];

            string[] v = input.ReadLine().Split(' ');

            for (int j = 0; j < v.Length; j++) commands[j] = long.Parse(v[j]);

            if (enableLogs) LogCommands(instructions, "");

            string str = "";

            Console.WriteLine($"inst l1: {instructions.Count}, rLength: {rLength}, commands: {commands.Length}");

            for (int j = 0; j < commands.Length; j++)
            {
                if (j != 0 && j % 500 == 0) Console.WriteLine($"{j}/{commandLength} ({j / (float)commandLength * 100}%) {DateTime.Now}");

                str += GetColor(instructions, commands[j]);
            }

            Console.WriteLine();
            Console.WriteLine(str);
            Console.WriteLine();

            output.WriteLine(str);
        }
    }

    private static List<Instruction> GetInstructions(TextReader input, out int rLength)
    {
        rLength = 0;

        Stack<(RepeatBuilder, long)> repeats = new Stack<(RepeatBuilder, long)>();

        List<Instruction> instructions = new List<Instruction>();

        long totTime = 0;

        while (true)
        {
            rLength++;

            string[] vals = input.ReadLine().Split(' ');

            string command = vals[0];

            if (command == "SHOW")
            {
                long duration = long.Parse(vals[1]);

                if (vals[2].Length > 1) throw new Exception("Invalid input");

                Instruction inc = new Show(duration, totTime, vals[2][0]);
                totTime += duration;

                if (repeats.Count != 0) repeats.Peek().Item1.Add(inc);
                else instructions.Add(inc);
            }
            else if (command == "REPEAT")
            {
                RepeatBuilder rep = new RepeatBuilder(long.Parse(vals[1]), totTime);
                repeats.Push((rep, totTime));
                totTime = 0;
            }
            else if (command == "END")
            {
                if (repeats.Count != 0)
                {
                    (RepeatBuilder, long) data = repeats.Pop();

                    Repeat r = data.Item1.ToRepeat();

                    totTime = data.Item2 + r.Duration;

                    if (repeats.Count == 0) instructions.Add(r);
                    else repeats.Peek().Item1.Add(r);
                }
                else break;
            }
            else throw new Exception("Invalid command");
        }

        return instructions;
    }

    private static void LogCommands(List<Instruction> commands, string pref)
    {
        for (int i = 0; i < commands.Count; i++)
        {
            if ((commands[i] is Repeat))
            {
                Repeat r = (Repeat)(commands[i]);

                Console.WriteLine($"{pref}Repeat {r.repCount}x ({commands[i].Duration}), |{commands[i].TotalTime}| - {r.RepDuration}");
                LogCommands(r.instructions, pref + " -");
                continue;
            }

            Console.WriteLine($"{pref}{commands[i]} ({commands[i].Duration}), |{commands[i].TotalTime}|");
        }
    }

    public static void _Main()
    {
        using var vstup = new StreamReader(File.OpenRead("C:\\Users\\kocna\\Downloads\\A.txt"));
        using var vystup = new StreamWriter(File.OpenWrite("C:\\Users\\kocna\\Downloads\\A-vysledek.txt"));

        PrectiVstup(vstup, vystup);
    }
}

public static class Handler
{
    public static void Main()
    {
        
    }
}