using System;
using System.IO;

class Program {
    static void Main() {
        string dir = @"c:\Ander\Workspace\C\proiektuak\tentsiometro_digitala\tentsiometro_digitala\audioak";
        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }
        string path = Path.Combine(dir, "hustu.wav");
        
        int sampleRate = 44100;
        short channels = 1;
        short bitsPerSample = 16;
        int durationSeconds = 3; // Loop for deflating
        int samples = sampleRate * durationSeconds;
        
        byte[] wavFile = new byte[44 + samples * 2];
        
        // WAV Header
        Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes("RIFF"), 0, wavFile, 0, 4);
        BitConverter.GetBytes(36 + samples * 2).CopyTo(wavFile, 4);
        Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "), 0, wavFile, 8, 8);
        BitConverter.GetBytes(16).CopyTo(wavFile, 16);
        BitConverter.GetBytes((short)1).CopyTo(wavFile, 20);
        BitConverter.GetBytes(channels).CopyTo(wavFile, 22);
        BitConverter.GetBytes(sampleRate).CopyTo(wavFile, 24);
        BitConverter.GetBytes(sampleRate * channels * (bitsPerSample / 8)).CopyTo(wavFile, 28);
        BitConverter.GetBytes((short)(channels * (bitsPerSample / 8))).CopyTo(wavFile, 32);
        BitConverter.GetBytes(bitsPerSample).CopyTo(wavFile, 34);
        Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes("data"), 0, wavFile, 36, 4);
        BitConverter.GetBytes(samples * 2).CopyTo(wavFile, 40);
        
        Random rand = new Random();
        double filterState1 = 0;
        
        for (int i = 0; i < samples; i++) {
            // Zarata zuria
            double noise = (rand.NextDouble() * 2.0 - 1.0); 
            
            // Iragazki sinpleagoa eta frekuentzia altuagoekin (hiss argiagoa)
            filterState1 += (noise - filterState1) * 0.15; 

            // Bolumena erdira jaitsi dugu, ertain bat lortzeko (8000 inguru)
            short sample = (short)(filterState1 * 8000); 
            
            BitConverter.GetBytes(sample).CopyTo(wavFile, 44 + i * 2);
        }
        
        File.WriteAllBytes(path, wavFile);
        Console.WriteLine("hustu.wav generated successfully.");
    }
}
