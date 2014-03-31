using UnityEngine;
using System.Collections;

public class JavelinAbility : ActiveAbility {

	public GameObject javelin_prefab;

	protected override void OnActivate () {
		if(character.backpack.Get_Javelin() > 0){
			GameObject new_javelin;
			int aim;
			if(character.dir == EntityBase.Direction.Left){
				new_javelin = (GameObject)Instantiate(javelin_prefab,
				                                      character.transform.position + new Vector3(1, 2f, 0),
				                                      transform.rotation);
				aim = (int) GlobalConstant.direction.left;
			}
			else{
				new_javelin = (GameObject)Instantiate(javelin_prefab,
				                                      character.transform.position + new Vector3(-1, 2f, 0),
				                                      transform.rotation);
				aim = (int) GlobalConstant.direction.right;
			}
			
			new_javelin.GetComponent<JavelinControl>().Create_Javelin(character.gameObject, aim);
			character.backpack.remove_jevelin(1);

			//Change this to be called after the animation finishes, if implemented
			character.gameObject.GetComponent<Player> ().AbilityAnimationFinished ("JavelinAbility");
		}
	}
}
