namespace Aura8;



public class CPU {
    public Action[] functions;

    public byte[] memory;
    public byte[] stack;
    public byte IP; // Instruction ptr
    public byte SP; // Stack ptr
    public byte A;
    public byte X;
    public byte Y;
    public byte Z;

    public struct Flags {
        public byte CARRY;
        public byte ZERO;
        public byte TRUEFALSE;
        public byte INTERRUPT_DISABLE;
        public byte DECIMAL_MODE;
        public byte HALT;
        public byte OVERFLOW;
        public byte NEGATIVE;

        public void Reset() {
            CARRY = 0;
            ZERO = 0;
            TRUEFALSE = 0;
            INTERRUPT_DISABLE = 0;
            DECIMAL_MODE = 0;
            HALT = 0;
            OVERFLOW = 0;
            NEGATIVE = 0;
        }

        public byte STATUS {
            get {
                return (byte)
                    (CARRY << 0 |
                    ZERO << 1 |
                    TRUEFALSE << 2 |
                    INTERRUPT_DISABLE << 3 |
                    DECIMAL_MODE << 4 |
                    HALT << 5 |
                    OVERFLOW << 6 |
                    NEGATIVE << 7);
            }
            set {
                CARRY = (byte)(value & 1);
                ZERO = (byte)(value & 2);
                TRUEFALSE = (byte)(value & 4);
                INTERRUPT_DISABLE = (byte)(value & 8);
                DECIMAL_MODE = (byte)(value & 16);
                HALT = (byte)(value & 32);
                OVERFLOW = (byte)(value & 64);
                NEGATIVE = (byte)(value & 128);
            }
        }
    }
    public Flags flags;

    public CPU() {
        memory = new byte[256];
        stack = new byte[256];
        functions = new Action[256];
        LoadFunctions();
        Reset();
    }

    public void Reset() {
        Array.Clear(memory);
        Array.Clear(stack);
        IP = 0;
        SP = 0;
        A = 0;
        X = 0;
        Y = 0;
        Z = 0;
        flags.Reset();
    }

    public void Execute() {
        while(flags.HALT == 0) {
            byte instruction = memory[IP];
            if(functions[instruction] == null) {
                Console.WriteLine($"No instruction found for: {instruction} | 0x{instruction:X}");
                flags.HALT = 1;
                break;
            }
            functions[instruction]();
        }
    }

    public void SetZeroFlag(byte val) {
        flags.ZERO = Convert.ToByte(val == 0);
    }

    public void SetTrueFalseFlag(bool exp) {
        flags.TRUEFALSE = Convert.ToByte(exp);
    }

    public void LoadFunctions() {
        // Basic
        functions[(byte)Instruction.HALT] = () => {
            flags.HALT = 1;
        };
        functions[(byte)Instruction.NOP] = () => {
            IP++;
        };
        functions[(byte)Instruction.BREAK] = () => {
            // ?
            // Push CPU state onto stack
            // Goto exception handler. IP = wherever
        };

        // Logic
        functions[(byte)Instruction.NOT] = () => {
            A = (byte)~A;
            SetZeroFlag(A);
            IP++;
        };
        functions[(byte)Instruction.AND] = () => {
            A &= A;
            SetZeroFlag(A);
            IP++;
        };
        functions[(byte)Instruction.OR] = () => {
            A |= A;
            SetZeroFlag(A);
            IP++;
        };
        functions[(byte)Instruction.XOR] = () => {
            A ^= A;
            SetZeroFlag(A);
            IP++;
        };

        // Arithmetic
        functions[(byte)Instruction.ADD] = () => {
            A += Z;
            SetZeroFlag(A);
            IP++;
        };
        functions[(byte)Instruction.SUB] = () => {
            A -= Z;
            SetZeroFlag(A);
            IP++;
        };
        functions[(byte)Instruction.MUL] = () => {
            A *= Z;
            SetZeroFlag(A);
            IP++;
        };
        functions[(byte)Instruction.DIV] = () => {
            byte q = (byte)(A % Z);
            A /= Z;
            Z = q;
            SetZeroFlag(A);
            IP++;
        };

        // Inc/Dec
        functions[(byte)Instruction.INC] = () => {
            A += 1;
            SetZeroFlag(A);
            IP++;
        };
        functions[(byte)Instruction.DEC] = () => {
            A -= 1;
            SetZeroFlag(A);
            IP++;
        };

        // Set registers
        functions[(byte)Instruction.SET_A_VAL] = () => {
            A = memory[++IP];
            SetZeroFlag(A);
            IP++;
        };
        functions[(byte)Instruction.SET_X_VAL] = () => {
            X = memory[++IP];
            SetZeroFlag(X);
            IP++;
        };
        functions[(byte)Instruction.SET_Y_VAL] = () => {
            Y = memory[++IP];
            SetZeroFlag(Y);
            IP++;
        };
        functions[(byte)Instruction.SET_Z_VAL] = () => {
            Z = memory[++IP];
            SetZeroFlag(Z);
            IP++;
        };

        // Stack
        functions[(byte)Instruction.PUSH_STATUS] = () => {
            stack[SP++] = flags.STATUS;
            IP++;
        };
        functions[(byte)Instruction.PUSH_A] = () => {
            stack[SP++] = A;
            IP++;
        };
        functions[(byte)Instruction.PUSH_X] = () => {
            stack[SP++] = X;
            IP++;
        };
        functions[(byte)Instruction.PUSH_Y] = () => {
            stack[SP++] = Y;
            IP++;
        };
        functions[(byte)Instruction.PUSH_Z] = () => {
            stack[SP++] = Z;
            IP++;
        };
        functions[(byte)Instruction.PULL_STATUS] = () => {
            flags.STATUS = stack[--SP];
            IP++;
        };
        functions[(byte)Instruction.PULL_A] = () => {
            A = stack[--SP];
            IP++;
        };
        functions[(byte)Instruction.PULL_X] = () => {
            X = stack[--SP];
            IP++;
        };
        functions[(byte)Instruction.PULL_Y] = () => {
            Y = stack[--SP];
            IP++;
        };
        functions[(byte)Instruction.PULL_Z] = () => {
            Z = stack[--SP];
            IP++;
        };

        // Clear flags
        functions[(byte)Instruction.CLEAR_STATUS] = () => {
            flags.STATUS = 0;
            IP++;
        };
        functions[(byte)Instruction.CLEAR_CARRY] = () => {
            flags.CARRY = 0;
            IP++;
        };
        functions[(byte)Instruction.CLEAR_DECIMAL] = () => {
            flags.DECIMAL_MODE = 0;
            IP++;
        };
        functions[(byte)Instruction.CLEAR_INTERRUPT] = () => {
            flags.INTERRUPT_DISABLE = 0;
            IP++;
        };
        functions[(byte)Instruction.CLEAR_OVERFLOW] = () => {
            flags.OVERFLOW = 0;
            IP++;
        };

        // Set flags
        functions[(byte)Instruction.SET_STATUS] = () => {
            flags.STATUS = A;
            IP++;
        };
        functions[(byte)Instruction.SET_CARRY] = () => {
            flags.STATUS = 1;
            IP++;
        };
        functions[(byte)Instruction.SET_DECIMAL] = () => {
            flags.DECIMAL_MODE = 1;
            IP++;
        };
        functions[(byte)Instruction.SET_INTERRUPT] = () => {
            flags.INTERRUPT_DISABLE = 1;
            IP++;
        };
        functions[(byte)Instruction.SET_OVERFLOW] = () => {
            flags.OVERFLOW = 1;
            IP++;
        };

        // Compares
        functions[(byte)Instruction.IS_EQ] = () => {
            SetTrueFalseFlag(A == Z);
            IP++;
        };
        functions[(byte)Instruction.IS_GR] = () => {
            SetTrueFalseFlag(A > Z);
            IP++;
        };
        functions[(byte)Instruction.IS_GRE] = () => {
            SetTrueFalseFlag(A >= Z);
            IP++;
        };
        functions[(byte)Instruction.IS_LS] = () => {
            SetTrueFalseFlag(A < Z);
            IP++;
        };
        functions[(byte)Instruction.IS_LSE] = () => {
            SetTrueFalseFlag(A <= Z);
            IP++;
        };

        // Jumps
        functions[(byte)Instruction.JUMP] = () => {
            IP = A;
        };
        functions[(byte)Instruction.JUMP_TRUE] = () => {
            if(flags.TRUEFALSE == 1) {
                IP = A;
            }
            else {
                IP++;
            }
        };
        functions[(byte)Instruction.JUMP_FALSE] = () => {
            if(flags.TRUEFALSE == 0) {
                IP = A;
            }
            else {
                IP++;
            }
        };
        
    }
}
