package org.example;

import java.io.*;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.Scanner;
import java.util.concurrent.TimeUnit;

public class Client {
    private Socket socket;
    private static ObjectInputStream inStream = null;
    private static ObjectOutputStream outStream = null;
    private static int clientCounter = 0;
    private int clientId;

    public Client(String address, int port) throws IOException {
        try {
            socket = new Socket(address, port);
            outStream = new ObjectOutputStream(socket.getOutputStream());
            inStream = new ObjectInputStream(socket.getInputStream());
            this.clientId = ++clientCounter;
        }
        catch (UnknownHostException u) {
            System.out.println(u);
        }
    }


    public static void main(String[] args) {
        Client client = null;
        try {
            client = new Client("localhost", 5000);
            System.out.println("Connected with server!");

            Message msg1 = client.receiveMessage();
            System.out.println(msg1.getContent());

            if (msg1.getContent().equals("ready")) {
                int n = client.getRandomNumber(1,10);
                System.out.println("Sending "+n+" messages");
                client.sendMessage(new Message(client.clientId, Integer.toString(n)));
                Message msg2 = client.receiveMessage();

                for (int i = 0; i < n; i++) {
                    String messageContent = String.valueOf(getRandomNumber(1,100));
                    client.sendMessage(new Message(client.clientId,messageContent));
                }

                Message msg3 = client.receiveMessage();
                System.out.println(msg3.getContent());
            }
        } catch (IOException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        } finally {
            client.closeStreams();
        }
    }

    public void sendMessage(Message message) {
        try {
            message.setNumber(clientId);
            outStream.writeObject(message);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }


    public Message receiveMessage() {
        try {
            return (Message) inStream.readObject();
        } catch (IOException | ClassNotFoundException e) {
            e.printStackTrace();
            return null;
        }
    }

    private void closeStreams() {
        try {
            outStream.close();
            inStream.close();
            socket.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }


    public static ObjectInputStream getInStream() {
        return inStream;
    }

    public static void setInStream(ObjectInputStream inStream) {
        Client.inStream = inStream;
    }

    public static ObjectOutputStream getOutStream() {
        return outStream;
    }

    public static void setOutStream(ObjectOutputStream outStream) {
        Client.outStream = outStream;
    }

    public static int getRandomNumber(int min, int max) throws InterruptedException {
        TimeUnit.SECONDS.sleep(2);
        return (int) ((Math.random() * (max - min)) + min);
    }
}
