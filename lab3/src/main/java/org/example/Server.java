package org.example;

import java.awt.*;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Server {
    private ServerSocket serverSocket;
    private List<Thread> threadsList;
    private static int clientCounter = 0;

    public Server(int port) throws IOException {
        threadsList = new ArrayList<>();
        serverSocket = new ServerSocket(port);
        System.out.println("Server started on port " + port);

        while (true) {
            try {
                Socket clientSocket = serverSocket.accept();
                System.out.println("New client connected: " + clientSocket.getPort());

                ThreadCommunicator clientThread = new ThreadCommunicator(clientSocket, clientCounter++);
                Thread thread = new Thread(clientThread);
                threadsList.add(thread);
                thread.start();
            } catch (IOException e) {
                System.out.println("Accepting client error");
            }
        }
    }

    public static void main(String[] args) {
        try {
            Server server = new Server(5000);
        } catch (IOException e) {
            System.out.println("Creating server error");
        }
    }
}

