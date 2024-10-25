using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    // 클래스의 정적(Static) 인스턴스를 저장하는 변수입니다. static 변수이기 떄문에 클래스 전체에서 하나만 존재하며, 여러 곳에서 접근할 수 있습니다.
    // 이 변수는 클래스가 싱글톤으로 동작하도록 설정하는 핵심입니다.
    public static CharacterManager Instance
    // 정적 프로퍼티로, CharacterManager 인스턴스에 접근할 수 있도록 만들어졌습니다. 
    {
        get // get 접근자: 인스턴스가 null일 경우,새로운 **GameObject**를 생성하고 그 안에 CharacterManager 컴포넌트를 추가하여 인스턴스를 만듭니다. 이 방식으로 클래스가 최초로 호출될 때 자동으로 인스턴스화 됩니다.
        {
            if (_instance == null) 
            {
                _instance = new GameObject("CharacerManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }
    // 결과: 싱글톤 패턴을 통해 CharacterManager가 언제 어디서든 동일한 인스턴스를 반환하며, 게임 내에서 하나의 오브젝트로만 존재하게 됩니다.

    public Player Player
    // Player 클래스에 대한 정보(캐릭터 관련 정보)를 저장하는 프로퍼티입니다.
    // _player 필드: player 클래스의 인스턴스를 저장하는 비공개 필드입니다.
    {
        get { return _player; } // get 접근자: 현재 **_player**의 값을 반환합니다.
        set { _player = value; } // set 접근자: 전달된 Player 인스턴스를 **_player**에 설정합니다.
    }
    private Player _player;
    // 결과: 이 프로퍼티는 게임 내에서 캐릭터 정보를 관리하고 접근하는데 사용됩니다.

    private void Awake() // Unity의 생명 주기 메서드 중 하나로, 게임 오브젝트가 활성화되기 직전에 호출됩니다.
    {
        if (_instance == null)
        // 처음 CharacterManager가 생성될 때, _instance가 null이면 현재 오브젝트를 싱글톤 인스턴스로 설정합니다.
        {
            _instance = this;
            // 현재 오브젝트를 싱글톤 인스턴스로 설정합니다.
            DontDestroyOnLoad(gameObject);
            // 이 게임 오브젝트를 씬이 바뀌더라도 파괴되지 않도록 설정합니다. 이로 인해 씬 전환이 일어나도 CharacerManager가 계속 유지됩니다.
        }
        else
        {
            if (_instance != this)
            // _instance가 이미 존재하는 경우, 즉, 다른 CharacterManager가 이미 생성되어 있다면, 현재 오브젝트는 필요 없는 인스턴스이므로 삭제합니다.
            {
                Destroy(gameObject);
                // 중복된 CharacterManager 인스턴스를 삭제하여, 게임 내에 **오직 하나의 CharacterManager**만 존재하게 만듭니다.
            }
        }
        // 결과: CharacterManager는 하나의 인스턴스만 존재하며, 중복된 인스턴스가 생성되면 자동으로 삭제됩니다.
    }
}
