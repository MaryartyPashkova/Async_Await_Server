using System;
using System.IO.Pipes;
using System.IO;
using System.Threading;
using System.Collections.Generic;
//void ReaderNewClient(NamedPipeServerStream Pipe1)
//{

//}

int ClientCount = 0; 
void NewConnect()
{
    while (true)
    {
        NamedPipeServerStream Pipe1 = new NamedPipeServerStream("myPipe", PipeDirection.InOut, 15, PipeTransmissionMode.Message , PipeOptions.Asynchronous);
        Pipe1.WaitForConnection();
        ClientCount++;
        Console.WriteLine("Клиент подключился");
        Thread thread = new Thread(NewClient);
        thread.Start(Pipe1);
    }
}
 List<string> Mesages = new List<string>();

void readMes(StreamReader StreamReader)
{
    string m = StreamReader.ReadLine();
    Mesages.Add(m);
    Console.WriteLine("readMes");
}
async Task readMesAsync(StreamReader StreamReader)
{
    await Task.Run(() =>  readMes(StreamReader));
}
void NewClient(Object Pipe1Object)
{
    
    int countMsgs = 0;
    int cc = ClientCount;
    NamedPipeServerStream Pipe1 = (NamedPipeServerStream)Pipe1Object;
    StreamReader rd = new StreamReader(Pipe1);
    StreamWriter writer = new StreamWriter(Pipe1);
    writer.AutoFlush = true;
    Task readTask = readMesAsync(rd);
    while (true)
    {
        if (readTask.IsCompleted)
        {
            if (countMsgs < Mesages.Count())
            {
                for (int i = countMsgs; i < Mesages.Count(); i++)
                {
                    writer.WriteLine(Mesages[i]);
                    countMsgs++;
                    Console.WriteLine(cc+"  -  "+ Mesages[i]+ " - "+ i);
                }
            }
            readTask = readMesAsync(rd);
        }       
    }
}

Console.WriteLine("Server");
Thread thread = new Thread(NewConnect);
thread.Start();