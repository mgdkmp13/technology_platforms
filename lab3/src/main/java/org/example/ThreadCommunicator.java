package org.example;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import static java.lang.Integer.parseInt;

public class ThreadCommunicator implements Runnable {
    private final Socket clientSocket;
    private final ObjectInputStream inStream;
    private final ObjectOutputStream outStream;
    private final int clientId;

    public ThreadCommunicator(Socket clientSocket, int clientId) throws IOException {
        this.clientSocket = clientSocket;
        this.outStream = new ObjectOutputStream(clientSocket.getOutputStream());
        this.inStream = new ObjectInputStream(clientSocket.getInputStream());
        this.clientId = ++clientId;

    }

    @Override
    public void run() {
        try {
            sendMessage(new Message("ready"));
            Message msg1 = receiveMessage();
            sendMessage(new Message("ready for " + msg1.getContent() + " messages"));
            System.out.println("Ready for " + msg1.getContent() + " messages from client " + clientId);
            int messageCount = Integer.parseInt(msg1.getContent());
            for (int i = 0; i < messageCount; i++) {
                Message msg = receiveMessage();
                System.out.println("Message from client "+ clientId + " (port " +clientSocket.getPort() + "): " + msg.getContent());
            }
            sendMessage(new Message("finished"));
        } catch (IOException | ClassNotFoundException e) {
            System.out.println("Communication error");
        } finally {
            closeStreams();
        }
    }

    public void sendMessage(Message message) throws IOException {
        outStream.writeObject(message);
        outStream.flush();
    }

    public Message receiveMessage() throws IOException, ClassNotFoundException {
        return (Message) inStream.readObject();
    }

    private void closeStreams() {
        try {
            outStream.close();
            inStream.close();
            clientSocket.close();
        } catch (IOException e) {
            System.out.println("Closing streams error");
        }
    }
}

