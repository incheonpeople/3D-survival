using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    // Ŭ������ ����(Static) �ν��Ͻ��� �����ϴ� �����Դϴ�. static �����̱� ������ Ŭ���� ��ü���� �ϳ��� �����ϸ�, ���� ������ ������ �� �ֽ��ϴ�.
    // �� ������ Ŭ������ �̱������� �����ϵ��� �����ϴ� �ٽ��Դϴ�.
    public static CharacterManager Instance
    // ���� ������Ƽ��, CharacterManager �ν��Ͻ��� ������ �� �ֵ��� ����������ϴ�. 
    {
        get // get ������: �ν��Ͻ��� null�� ���,���ο� **GameObject**�� �����ϰ� �� �ȿ� CharacterManager ������Ʈ�� �߰��Ͽ� �ν��Ͻ��� ����ϴ�. �� ������� Ŭ������ ���ʷ� ȣ��� �� �ڵ����� �ν��Ͻ�ȭ �˴ϴ�.
        {
            if (_instance == null) 
            {
                _instance = new GameObject("CharacerManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }
    // ���: �̱��� ������ ���� CharacterManager�� ���� ��𼭵� ������ �ν��Ͻ��� ��ȯ�ϸ�, ���� ������ �ϳ��� ������Ʈ�θ� �����ϰ� �˴ϴ�.

    public Player Player
    // Player Ŭ������ ���� ����(ĳ���� ���� ����)�� �����ϴ� ������Ƽ�Դϴ�.
    // _player �ʵ�: player Ŭ������ �ν��Ͻ��� �����ϴ� ����� �ʵ��Դϴ�.
    {
        get { return _player; } // get ������: ���� **_player**�� ���� ��ȯ�մϴ�.
        set { _player = value; } // set ������: ���޵� Player �ν��Ͻ��� **_player**�� �����մϴ�.
    }
    private Player _player;
    // ���: �� ������Ƽ�� ���� ������ ĳ���� ������ �����ϰ� �����ϴµ� ���˴ϴ�.

    private void Awake() // Unity�� ���� �ֱ� �޼��� �� �ϳ���, ���� ������Ʈ�� Ȱ��ȭ�Ǳ� ������ ȣ��˴ϴ�.
    {
        if (_instance == null)
        // ó�� CharacterManager�� ������ ��, _instance�� null�̸� ���� ������Ʈ�� �̱��� �ν��Ͻ��� �����մϴ�.
        {
            _instance = this;
            // ���� ������Ʈ�� �̱��� �ν��Ͻ��� �����մϴ�.
            DontDestroyOnLoad(gameObject);
            // �� ���� ������Ʈ�� ���� �ٲ���� �ı����� �ʵ��� �����մϴ�. �̷� ���� �� ��ȯ�� �Ͼ�� CharacerManager�� ��� �����˴ϴ�.
        }
        else
        {
            if (_instance != this)
            // _instance�� �̹� �����ϴ� ���, ��, �ٸ� CharacterManager�� �̹� �����Ǿ� �ִٸ�, ���� ������Ʈ�� �ʿ� ���� �ν��Ͻ��̹Ƿ� �����մϴ�.
            {
                Destroy(gameObject);
                // �ߺ��� CharacterManager �ν��Ͻ��� �����Ͽ�, ���� ���� **���� �ϳ��� CharacterManager**�� �����ϰ� ����ϴ�.
            }
        }
        // ���: CharacterManager�� �ϳ��� �ν��Ͻ��� �����ϸ�, �ߺ��� �ν��Ͻ��� �����Ǹ� �ڵ����� �����˴ϴ�.
    }
}
