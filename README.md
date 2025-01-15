# 241125SpartaTeam
 스파르타 국비지원학원에서 Unity 6기 최종 프로젝트
 TopDownCotroller형식의 2D게임으로 제작이 가능한 대장장이RPG입니다.

## 목차

## 게임개요
## 게임소개

마인스톤 : 대장장이의 길
장르 : 2D TopDownRPG

신화 속 장비의 실마리를 찾고자 던전을 들어가는 대장장이의 이야기

- 조합
   장비가 없다구요? 저기 몬스터 보이시죠? 이젠 제 장비입니다.
   몬스터를 쓰러뜨리고 얻은 부산물로 나아갈 길을 만들어 보세요!
- 강화
   강화로 해결되지 않는다면 강화가 부족한 건 아닐지 생각해보자.
   너무나도 많은 강화로 몬스터를 쓰러뜨려보세요!

## 게임영상
## 팀원소개
- 이준형(팀장) :
- 이상운(부팀장) :
- 김준식 :
- 백승우 :
## 주요 사용 기술
## 기획
- 개발 일정
## 게임 미리보기 (InGame)

## 게임 맛보기
- 링크 : https://tkql0.itch.io/milestone-blacksmith-road
## 게임 기능
1. 캐릭터 생성 및 저장

![Create](https://github.com/user-attachments/assets/66df57ad-5daf-4da0-bb9e-9b098fc02a28)


   - (생성) 비어있음 > 캐릭터 생성 > 이름 입력 > 생성
   - (저장) ESC > 옵션 > 메인메뉴(자동저장)
2. 플레이어 입력

![4343](https://github.com/user-attachments/assets/8b380d23-1022-402b-9930-2dfee94752cb)

- 좌우이동 : WASD
- 상호작용 : E
  - NPC와 상호작용
- 왼쪽 마우스 : 공격
  - 근거리
  - 화살
  - 마법
- 오른쪽 마우스 : 방패로 막기(누르고 있기)
  - 스태미너 소모 후 방어력 2배 증가
- 구르기 : Space
  - 스태미너 소모 후 무적상태
- 인벤토리 : I
  - 플레이어가 먹은 아이템 저장
- 미니맵 컨트롤 : 0, 9
  - 미니맵
- 제작 : J
  - 무기나 방어구 제작
  
- 옵션 : ESC
   - 사운드 (BGM, SFX), 밝기 조절
     
![Brightness](https://github.com/user-attachments/assets/c5f07745-2f1d-4c7f-bd3a-16f7f631281c)
  
- 캐릭터 스텟 : K
  
![Stats](https://github.com/user-attachments/assets/c1060034-d83b-44fc-82d4-0b606bfdb51b)

- 게임 설명 : P
    - 스토리, 목표, 조작키 등을 볼 수 있다.
      
3. 퀘스트

![444](https://github.com/user-attachments/assets/d773b716-0863-4f61-913d-922462282918)

- 게임 시작 시 "퀘스트 도착!" 클릭

![3323232](https://github.com/user-attachments/assets/09f91079-1801-46a4-be07-7e17a5a54751)

- 설명 창 보고 게임 진행

 ![433333](https://github.com/user-attachments/assets/49a9f5e6-8446-4098-ab4f-2185c090c41d)

- 퀘스트 목록 (위)
4. 몬스터

![MonsterKill](https://github.com/user-attachments/assets/c3e406ad-c53a-4258-9db1-01a96898f243)


  
5. 마인

![MineKill](https://github.com/user-attachments/assets/4542b12c-f69c-4e12-a123-edeea3618773)

6. 인벤토리
- 장착

![EquipInfo](https://github.com/user-attachments/assets/78dcb045-a598-4b83-b392-6eaf9612e876)
  
- 조합
   - 재료를 모아서 무기나 방어구를 조합 할 수 있다.
  
![Craft](https://github.com/user-attachments/assets/15902a81-50a2-4787-a218-886f3ed5df56)

- 강화
    - 강화를 통해 더 좋은 무기를 만들 수 있으며 잘못하면 무기가 파괴된다.
  
![Enhence](https://github.com/user-attachments/assets/f7c313da-5150-4e05-9ad7-4dbfb21c9ab7)

 7. 상점

![UseShop](https://github.com/user-attachments/assets/62225116-0588-4ee5-9a57-448677b7d8c3)

![434343](https://github.com/user-attachments/assets/7662619b-feee-4446-9d4e-0cc0aa5d03e7)

- 왼쪽 (무기&방어구, 포션 상점)    오른쪽 (여관)
  
![4444](https://github.com/user-attachments/assets/4479bf4a-8482-46e5-9d24-3b77741c50d6)

- 무기 & 방어구, 포션 상점 (내부)
  - 전투에 필요한 무기나 포션등을 살 수 있다.
    
![333](https://github.com/user-attachments/assets/68bd6422-4c67-4d8b-8f4a-736fd7231ace)

- 여관 (내부)
  - 플레이어의 체력을 Max수치로 회복시켜준다.

![55](https://github.com/user-attachments/assets/bab6c7cc-d778-4278-bdb2-6691c86652c8)

- NPC 이용
 슬롯 클릭 > 수량 입력 > 구매

## 기술스택
1. 모노싱글톤

![4334](https://github.com/user-attachments/assets/287c20f9-6d3b-4aa0-a24e-d1c41f16b84d)

- SingleTon과 MonoBehaviour를 같이 상속 받을 수 없으므로 사용 > 어디서든 접근이 편하다.
  
2. 플레이어
- InputSystem
  - 인풋은 Invoke Unity Event를 통하여 구현하였다.
   
     ![333](https://github.com/user-attachments/assets/d2c5b9ac-c6a3-4b8f-8748-be489afeee21)
![555](https://github.com/user-attachments/assets/d16df0ed-1f3f-44a7-b10a-3fa2ba52099f)


- Inventory
 InventoryItem을 통해 정보를 받아오며 Json에 저장 합니다.
 Json에 저장된 정보를 통해 게임을 껐다 켜더라도 아이템 데이터를 잃지 않습니다.
  
  - 강화 > 강화 확률을 UGS에서 가져온 정보를 통해 랜덤가중치로 적용
  - 장착 > Dictionary<ItemType, Image>: 키를 ItemType으로 설정하여 장비 변경시 데이터를 찾기 쉽게 변경하고 관리
  - 조합 > UGS로 아이템 조합에 필요한 재료 정보를 받고 재료 차감과 아이템 생성을 관리
    
4. SpawnManager
- Monster
  - MonsterAI
    - AI 종류 > MeeleAI.cs, RangeAI.cs, ChargeAI.cs
    - MonsterState > 상태에 따라 패턴을 다르게 관리를 해줌
- Mine
  - MineFull.cs > 마인의 체력이 0이되면 MineFull이 SetActive(false)
  - Mine.cs > 30초마다 코루틴으로 죽어있으면 다시 살려줌
5. UGS
- DataManager.cs > 여기 생성자에서 구글 시트 데이터 로드해주고 데이터 테이블을 생성해줌
- UGS를 통해 고정된 데이터를 쉽게 관리함
- Item, MainQuest, Crafting, UI, Scene, PlayerStat, MonsterSpawn, Upgrade, Creatrue 데이터를 관리
6. 상점 NPC
- 
-
7. 최적화
   - ObjectPool
   - Atlas
   - ParticleSystem
## 프로젝트 와이어프레임 (전체적인)
![image](https://github.com/user-attachments/assets/2472737a-6511-46ea-8823-a10d13da21e6)
![image](https://github.com/user-attachments/assets/64db3e87-4709-4891-8009-afe0b54debff)


## 유저 테스트 피드백
- 프로젝트 개선
  - 장비의 밸런스 패치
  - 맵 버그 및 편의성 개선
  - 여관 시스템 추가
  - 아이템 자석 효과 추가
  - 공격시에만 마우스 방향을 바라보게 변경
  - 치트 모드 제거
    
- 개선해야 할 것
  - 해상도 조절
  - 몬스터 추가 후 테스트용 치트 돌광석 제거
  - UI
  - 공격 모션 
  - 무기 밸런스
    
## 트러블 슈팅
