using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class Backpack : MonoBehaviour {
    public int max_javelin_count;
    public int current_javelin_count;

	public interface Observer {
		void javelinCountChanged (int numJavelins);
	}
	private List<Observer> observers;

	public Backpack() {
		observers = new List<Observer> ();
	}

	public void AddObserver(Observer observer) {
		observers.Add (observer);
	}

	public int Get_Javelin(){
		return current_javelin_count;
	}

	private void NotifyObservers() {
		foreach (Observer observer in observers) {
			observer.javelinCountChanged(current_javelin_count);
		}
	}

    // adds to javelin count and then returns the new count
    public int add_javelin(int number) {
        if (current_javelin_count + number < max_javelin_count)
        {
            current_javelin_count += number;
        }
        else {
            current_javelin_count = max_javelin_count;
        }

		NotifyObservers ();

        return current_javelin_count;
    }

    // removes the number of javelins, then return the new count
    // if returned -1, then we currently have zero javelin and
    // can't remove anymore /BAD CODE
    public int remove_jevelin(int number) {
        if(current_javelin_count - number >= 0){
            current_javelin_count -= number;
        }
        else{
            return -1;
        }

		NotifyObservers ();

        return current_javelin_count;
    }
}
