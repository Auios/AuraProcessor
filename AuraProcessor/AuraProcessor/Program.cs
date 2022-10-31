namespace Aura8; 

static class Program {
    static void Main() {
        CPU cpu = new CPU();

        byte i = 0;
        cpu.memory[i++] = (byte)Instruction.SET_A_VAL;
        cpu.memory[i++] = 123;
        cpu.memory[i++] = (byte)Instruction.JUMP;
        cpu.memory[i++] = (byte)Instruction.HALT;

        cpu.Execute();

        Console.WriteLine($"========================");
        Console.WriteLine($"        NVHDITZC");
        Console.WriteLine($"Status: {Convert.ToString(cpu.flags.STATUS, 2).PadLeft(8, '0')}");
        Console.WriteLine($"A: {cpu.A}");
        Console.WriteLine($"X: {cpu.X}");
        Console.WriteLine($"Y: {cpu.Y}");
        Console.WriteLine($"Z: {cpu.Z}");
        Console.WriteLine($"IP: {cpu.IP}");
        Console.WriteLine($"SP: {cpu.SP}");
    }
}