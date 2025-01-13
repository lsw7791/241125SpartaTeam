# 241125SpartaTeam
 스파르타 국비지원학원에서 4명이서 만든 최종 프로젝트
 TopDownCotroller형식의 2D게임으로 제작이 가능한 대장장이RPG입니다.

## 목차

## 게임개요
## 게임소개
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
1. 플레이어 입력

![4343](https://github.com/user-attachments/assets/8b380d23-1022-402b-9930-2dfee94752cb)

- 좌우이동 : WASD
- 상호작용 : E
  - NPC와 상호작용
- 왼쪽 마우스 : 공격
- 오른쪽 마우스 : 방패로 막기(누르고 있기)
- 구르기 : Space
- 인벤토리 : I
- 미니맵 컨트롤 : 0, 9
- 제작 : J
- 옵션 : ESC

![Stats](https://github.com/user-attachments/assets/c1060034-d83b-44fc-82d4-0b606bfdb51b)

- 캐릭터 스텟 : K
- 게임 설명 : P
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
 
![Craft](https://github.com/user-attachments/assets/15902a81-50a2-4787-a218-886f3ed5df56)

- 강화
  
![Enhence](https://github.com/user-attachments/assets/f7c313da-5150-4e05-9ad7-4dbfb21c9ab7)

 7. 상점

![UseShop](https://github.com/user-attachments/assets/62225116-0588-4ee5-9a57-448677b7d8c3)

![434343](https://github.com/user-attachments/assets/7662619b-feee-4446-9d4e-0cc0aa5d03e7)

- 왼쪽 (무기&방어구, 포션 상점)    오른쪽 (여관)
  
![4444](https://github.com/user-attachments/assets/4479bf4a-8482-46e5-9d24-3b77741c50d6)

- 무기 & 방어구, 포션 상점 (내부)

![333](https://github.com/user-attachments/assets/68bd6422-4c67-4d8b-8f4a-736fd7231ace)

- 여관 (내부)

![55](https://github.com/user-attachments/assets/bab6c7cc-d778-4278-bdb2-6691c86652c8)

- NPC 이용
 슬롯 클릭 > 수량 입력 > 구매

## 기술스택
1. 모노싱글톤
2. 플레이어
   - InputSystem
   - Inventory
   - 강화 > 가중치
   - 장착
   - 조합
3. 몬스터
몬스터 스포너
상태
마인
4. UGS
5. 상점 NPC
6. 최적화
   - ObjectPool
   - Atlas
## 프로젝트 와이어프레임 (전체적인)

## 유저 테스트
- 프로젝트 개선
## 트러블 슈팅
