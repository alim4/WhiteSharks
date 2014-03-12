using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


	//Volume
	public static float sfxVolume = 1.0f;
	public static float musicVolume = 1.0f;
	public static float dialogueVolume = 1.0f;

	public enum SoundType { Sfx, Music, Dialogue }

	private AudioClip curClip = null;
	private GameObject musicObj;




	//From what I understand, this creates an instance of soundmanager once and then allows it to be called
	private static SoundManager s_instance = null;
	public static SoundManager instance {
		get {
			if(s_instance == null) {
				GameObject go = new GameObject("SoundManager");
				s_instance = go.AddComponent<SoundManager>();
			}
			return s_instance;
		}
	}


	/*Plays 2D Sound Effect by creating an empty object and attaching a sound clip to it. 
	Then it plays the clip and deletes the object after its done playing
	 * 
	 * 
	 * To call, simply run the code below with the soundfile in Resources/SoundEffects
	 * SoundManager.instance.Play2DSound((AudioClip)Resources.Load("Sounds/SoundEffects/SOUND_FILE_NAME"), SoundManager.SoundType.Sfx);
	 */
	public void Play2DSound(AudioClip clip, SoundType type) {
		AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
		GameObject newObj = new GameObject("SoundPlayer");
		newObj.transform.position = Camera.main.transform.position;
		newObj.transform.parent = Camera.main.transform;
		AudioSource newSource = newObj.AddComponent<AudioSource>() as AudioSource;
		newSource.audio.clip = clip;
		newSource.audio.volume = Volume(newSource, type);
		newSource.audio.Play();
		StartCoroutine("DeleteSource", newObj);
	}


	/*Plays 2D Music by selecting the empty object defined at the top and attaching a sound clip to it. 
	Then it plays the clip and loops it until another music is defined, or the KillMusic function is called.
	If something will try to play a song, it will ignore it unless it is a different song, in which case, it 
	will kill the music and play the new song.
	 * 
	 * 
	 * To call, simply run the code below with the soundfile in Resources/Music
	 * SoundManager.instance.Play2DMusic((AudioClip)Resources.Load("Sounds/Music/SOUND_FILE_NAME"));
	 */
	public void Play2DMusic(AudioClip clip) {
		if(clip == curClip) return;
		else if(musicObj != null) {
			KillMusic();
		}
		musicObj = new GameObject("SoundPlayer");
		musicObj.transform.position = Camera.main.transform.position;
		musicObj.transform.parent = Camera.main.transform;
		curClip = clip;
		AudioSource newSource = musicObj.AddComponent<AudioSource>() as AudioSource;
		newSource.audio.clip = clip;
		newSource.audio.loop = true;
		newSource.audio.volume = Volume(newSource, SoundType.Music);
		newSource.audio.Play();
	}


	//Kills current music
	public void KillMusic() {
		Destroy(musicObj);
		curClip = null;
	}

	//Pauses current music
	public void PauseMusic() {
		musicObj.audio.Pause();
	}

	//Resumes current music
	public void ResumeMusic() {
		musicObj.audio.Play();
	}



	//Coroutine for sound effects that gets rid of audio source when audio is done playing
	IEnumerator DeleteSource(GameObject obj) {
		while(obj.audio.isPlaying)
			yield return null;
		Destroy(obj);
	}


	//Controls volume of sounds
	float Volume(AudioSource audio, SoundType type) {
		if(type == SoundType.Sfx) {
			return audio.volume * sfxVolume;
		}
		else if(type == SoundType.Music) {
			return audio.volume * musicVolume;
		}
		else if(type == SoundType.Dialogue) {
			return audio.volume * dialogueVolume;
		}
		return -1f;
	}
}
