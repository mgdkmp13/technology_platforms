package org.example;

import java.io.Serializable;

public class Message implements Serializable {
    private int number;
    private String content;
    private static final int num =0;


    public Message(){
        this.number = num+1;
        this.content = "";
    }
    public Message(String content){
        this.number = num+1;
        this.content = content;
    }

    public Message(int clientNumber, String content){
        this.number = clientNumber;
        this.content = content;
    }

    int getNumber(){
        return this.number;
    }

    String getContent(){
        return this.content;
    }

    void setNumber(int num){
        this.number = num;
    }

    void setContent(String cont){
        this.content = cont;
    }


}