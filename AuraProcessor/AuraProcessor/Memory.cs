namespace Aura8; 
public class Memory {
    byte[] data;

    public Memory(uint capacity) {
        data = new byte[capacity];
    }

    public byte Read(uint location) {
        return data[location];
    }

    public void Write(uint location, byte value) {
        data[location] = value;
    }
}
