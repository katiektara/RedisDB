using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using redisU.utils;
using redisU.framework;
using redisU.events;

public class Menu : MonoBehaviour {

    private static RedisConnection redis = null;

    public Text txt_output;
    public InputField input_key;
    public InputField input_value;
    public GameObject btn_create;
    public GameObject btn_read;
    public GameObject btn_update;
    public GameObject btn_delete;
    public GameObject btn_search;
 
    void Start() {
        try {
            //connect
            redis = new RedisConnection("localhost", 6379);
            //clear
            //redis.ClearAllDB();
            txt_output.text = "Redis ready";
        }
        catch {
            txt_output.text = "Redis went wrong";
        }
    }
    //delete sesion
    public void DeleteSession() {
        redis.EndConnection();
        Application.Quit();
    }
    //create
    public void Creating() {
        if ((input_key.text != "") && (input_value.text != "")) {
            if (redis.Get<string, string>(input_key.text) == null) {
                redis.Set<string, string>(input_key.text, input_value.text);
                input_key.text = "";
                input_value.text = "";
                txt_output.text = "Creating successful";
            } else {
                txt_output.text = "Key already exist";
            }
        }
        else {
            txt_output.text = "Fill in all the fields";
        };
    }
    //read
    public void Reading() {
        if (input_key.text != "") {
            if (redis.Get<string, string>(input_key.text) != null) {
                input_value.text = redis.Get<string, string>(input_key.text);
                txt_output.text = "Reading successful";
            } else {
                txt_output.text = "Key not found";
            }
        }
        else {
            txt_output.text = "Fill field 'input key'";
        };
    }
    //update
    public void Updating() {
        if ((input_key.text != "") && (input_value.text != "")) {
            if (redis.Get<string, string>(input_key.text) != null) {
                redis.Set<string, string>(input_key.text, input_value.text);
                input_key.text = "";
                input_value.text = "";
                txt_output.text = "Updating successful";
            } else {
                txt_output.text = "Key not found";
            }
        }
        else {
            txt_output.text = "Fill in all the fields";
        };
    }
    //delete
    public void Deleting() {
        if (input_key.text != "") {
            if (redis.Get<string, string>(input_key.text) != null) {
                redis.DeleteKeys<string>(input_key.text);
                input_key.text = "";
                txt_output.text = "Deleting successful";
            } else {
                txt_output.text = "Key not found";
            }
        }
        else {
            txt_output.text = "Fill field 'input key'";
        }
    }
    //search
    public string[] keys;
    public string[] values;
    public void Searching() {
        if (input_value.text != "") {
            keys = redis.GetKeysByPattern("*");
            values = redis.MultiGet<string, string>(keys);
        }
        else {
            txt_output.text = "Fill field 'input value'";
        }
    }

}
