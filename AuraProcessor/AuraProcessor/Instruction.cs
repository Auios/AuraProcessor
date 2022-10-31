namespace Aura8;

public enum Instruction {
    // Basic
    HALT = 0, // Stop CPU
    NOP, // Just increment IP
    BREAK, // 

    // Logic
    NOT = 10, // A = !A
    AND, // A = A & Z
    OR, // A = A | Z
    XOR, // A = A ^ Z

    // Arithmetic
    ADD = 20, // A = A + Z
    SUB, // A = A - Z
    MUL, // A = A * Z
    DIV, // A = A / Z, Z = Quotient

    // Inc/Dec
    INC = 30, // A -= 1
    DEC, // A += 1

    // Bitshift
    SHL = 40, // A = A << Z, Set zero flag 
    SHR, // A = A >> Z

    // Set registers 
    SET_A_VAL = 50, // A = val
    SET_X_VAL, // X = val
    SET_Y_VAL, // Y = val
    SET_Z_VAL, // Z = val

    // Stack
    PUSH_STATUS = 60, // Push copy of status onto stack
    PUSH_A,
    PUSH_X,
    PUSH_Y,
    PUSH_Z,
    PULL_STATUS,
    PULL_A,
    PULL_X,
    PULL_Y,
    PULL_Z,

    // Clear flags
    CLEAR_STATUS = 70, // Set all status flags to 0
    CLEAR_CARRY, // Clear carry flag
    CLEAR_DECIMAL, // Clear decimal mode flag
    CLEAR_INTERRUPT, // Clear interrupt disable flag
    CLEAR_OVERFLOW, // Clear overflow flag

    // Set flags
    SET_STATUS = 75, // Set status = A
    SET_CARRY, // Set carry flag
    SET_DECIMAL, // Set decimal mode flag
    SET_INTERRUPT, // Set interrupt disable flag
    SET_OVERFLOW, // Set overflow flag

    // Compares
    IS_EQ = 80, // Set truefalse flag if A == Z
    IS_GR, // Set truefalse flag if A > Z
    IS_GRE, // Set truefalse flag if A >= Z
    IS_LS, // Set truefalse flag if A < Z
    IS_LSE, // Set truefalse flag if A <= Z

    // Jumps
    JUMP = 90, // Jump. Set IP = A
    JUMP_TRUE, // Jump if truefalse flag is true. Set IP = A
    JUMP_FALSE, // Jump if truefalse flag is false. Set IP = A
}