# <span style="color:red"> 변경사항</span>

1. 기술기획서 작성

# (게임이름) 기술기획서 Template

## Class name here

- Class discription here

## Field name here

- Field discription here

## Method name here

- Method discription here

# 기술기획서 본문
# 게임 진행 관련 클래스

## StageManager : Monobehaviour

- 전투 스테이지 내의 진행을 관리하는 클래스로 스테이지 클리어 여부를 판단한다.

## Field
## Method

- void Update() 게임의 클리어 여부를 확인해 스테이지를 종료한다.

## AbsFactory

- 유닛을 소환하는 팩토리의 추상 클래스. 이를 상속한 각 팩토리에서 소환 구현.

## Field

## Method

- void MakeUnit(Position pos) 유닛을 소환하는 추상 메소드.

## projectileFactory

- 투사체를 소환하는 팩토리의 추상 클래스. 

## Field

## Method

- void MakeProjectile()

## CampaignManager

- 게임 전체에 영향을 미치는 이벤트(특정 스테이지 클리어 등) 관리하는 클래스.

## Field

-list<Flag> Flags

## Method

- void setFlag(Flag flag) 특정 이벤트가 일어났음을 기록
- bool getFlag(Flag flag) 특정 이벤트가 일어났는지 응답

## SaveManager

- 게임의 세이브 파일을 관리한다.

## Field

- FileStream saveData;

## Method

-void Save(string saveName) 현재 게임 상태를 파일에 저장한다.
-void Load(string saveName) 세이브파일을 불러온다.

## 오브젝트 관련 클래스
## Unit : Monobehaviour

- 플레이어, 적, 아군 유닛, 건물 등의 기초가 되는 추상 클래스.

## Field

- protected unsigned int maxHealth
- protected unsigned int curHealth
- protected unsigned int defense
- protected unsigned int attack
- protected unsigned int speed
- protected unsigned int range
- Projectile projectile
- Race race 유닛의 종족값(언데드 등)
- List<Buff> Buffs 유닛이 받고 있는 디버프/버프

## Method
- void Damage(int damage, Unit attacker) 유닛이 데미지를 입음, 결과적으로 사망하면 Die() 호출
- void Attack(Unit Target) 다른 유닛을 근접 공격함
- void Shoot(Unit Target) 다른 유닛을 향해 투사체/마법 발사
- void Shoot(Position pos) 특정 위치를 향해 투사체/마법 발사
- void Move(Position pos) 특정 위치를 향해 이동
- void Die() 유닛이 죽었을 때 발생하는 이벤트 등을 처리함(CampaignManager에 플래그를 세우는 등)
- public abstract int getNumofUnit() 해당 유닛의 개수 반환

## Player : Unit

- 플레이어 클래스이다.

## Field

- unsigned int level
- unsigned int experience
- unsigned int spellPoint
- unsigned int maxSpellPoint
- Skill skill
- Skill skill2
- Skill skill3
- unsigned int MaxNotch

## Method

- void Update() 아군 유닛에게 이동 명령
- public void UseSkill(Skill skill)
- public void UseSkill(Skill skill, Unit target)
- public void UseSkill(Skill skill, Position pos)
- public void GetExperience(unsigned int exp) 
- void LevelManage() 레벨업 시 관련 수치를 업데이트함
- public delegate void Command(Position pos) 아군 유닛들에게 이동 명령
- public override int getNumofUnit() 1 반환

## NPC : Unit

- 적과 아군 유닛들(플레이어 제외)의 부모 클래스가 되는 추상 클래스로 특수 공격과 사용 확률을 관리한다.

## Field

- AI ai
- Skill skill
- unsigned float RateOfSpecialAttack

## Method

- specialAttack(Unit target)
- specialAttack(Position pos)

## Enemy : NPC

- 적 유닛 클래스.

## Field

- unsigned const int exp
- private unsigned static int num 유닛의 개수(구체 클래스에만 만들 것)

## Method

- public override int getNumofUnit() 유닛의 개수 반환(구체 클래스에만 만들 것)

## Minion : NPC

- 아군 유닛 클래스.
- private unsigned static int num 유닛의 개수(구체 클래스에만 만들 것)

## Field

- unsigned const int notch

## Method’

- Awake() 플레이어 델리게이트에 스스로의 Move 추가(ai에 옮길 수도 있음)
- public int getNotch() 노치값 반환
- public override int getNumofUnit() 유닛의 개수 반환(구체 클래스에만 만들 것)
- OnDestroy() 플레이어 델리게이트에서 스스로의 Move 삭제(Awake()와 같이 ai에 옮길 수도 있음)

## Building : Unit

- 건물 클래스.

## Field

- unsigned readonly int maxSpawn 스폰 가능한 최대 유닛의 수
- unsigned int curSpawn 현재까지 스폰시킨 유닛의 수
- unsigned readonly int spawnLimit 맵 상에 있을 수 있는 최대 유닛 수
- Enemy spawnEnemy
- unsigned float spawnRate

## Method

- Damage(int damage, Unit attacker) 일반 공격에 데미지를 입지 않도록 오버라이드
- Spawn() 유닛 소환(팩토리 거쳐서), 현재 제한과 건물 스프라이트 등 조정

## Projectile : Monobehaviour

- 투사체 클래스.

## Field
- unsigned float speed
- unsigned float duration 날아가는 시간
- unsigned int damage

## Method

## EffectedProjectile : Projectile

- 특수 효과를 가지는 투사체 클래스, 장판이나 범위 버프 등 포함.

## Field

- unsigned float effectRadius

## Method

- OnTriggerStay()
- Effect() 트리거 내의 대상에게 효과 발동 및 유지

## Buff

- 버프/디버프 효과, 도트나 힐, 공격력/방어력 배수 등.

## Field

- unsigned float AttackFactor
- unsigned float DefenseFactor

## Method

- public int getAttackFactor()
- public int getDefenseFactor()
- public int updateBuff(Unit subject) 일정 시간마다 주는 효과

# 입력 관리 클래스
## KeyInputManager : Monobehaviour

## Field

## Method

## MouseInputManager : Monobehaviour

## Field

## Method

