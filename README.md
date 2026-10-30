# MyDefence · 타워 디펜스 학습 프로젝트

타워를 선택해 설치하고 적 웨이브에 대응하는 Unity 타워 디펜스 학습 프로젝트입니다.

## 학습·재구현 대상

- 타워 선택과 타일 기반 설치 UI
- 타워 강화 비용과 자원 처리
- 머신건·로켓·레이저 타워와 투사체
- 웨이브 생성과 적 이동 및 체력 UI

## 주요 코드와 구조

| 경로 | 역할 |
| --- | --- |
| [Assets/MyDefence/Scripts/BuildManager.cs](Assets/MyDefence/Scripts/BuildManager.cs) | 타워 선택과 타일 UI·강화 비용 |
| [Assets/MyDefence/Scripts/SpawnManager.cs](Assets/MyDefence/Scripts/SpawnManager.cs) | 웨이브와 적 생성 |
| [Assets/MyDefence/Scripts/TowerBlueprint.cs](Assets/MyDefence/Scripts/TowerBlueprint.cs) | 타워 프리셋 |
| [Assets/MyDefence/Scripts/UI](Assets/MyDefence/Scripts/UI) | 게임 UI |
| [Assets/MyDefence/Scenes](Assets/MyDefence/Scenes) | 메뉴와 플레이 씬 |
| [Assets/_Sample](Assets/_Sample) | 개별 개념 실습 |

## 실행 방법

1. Unity Hub에서 저장소 폴더를 프로젝트로 추가합니다.
2. **Unity 6000.3.13f1**로 열고 패키지 복원과 임포트를 기다립니다.
3. [시작 씬](Assets/MyDefence/Scenes/MainMenu.unity)을 엽니다.
4. Play Mode에서 동작을 확인합니다.


## 학습 방식과 AI 활용

수업 예제를 직접 제작한 뒤 수업에서 사용한 에셋과 관련 자료를 바탕으로 AI를 활용해 같은 기능을 다시 구현하는 학습을 진행했습니다. 수업 실습과 AI를 활용한 재구현 작업이 함께 포함된 학습 과정입니다.

| 구분 | 내용 |
| --- | --- |
| 학습 기반 | 수업에서 다룬 게임 기능과 사용 에셋 |
| AI 활용 | 기존 학습 내용을 바탕으로 기능을 재구현하는 과정에 활용 |
| 자료 성격 | 게임 동작과 구현 구조를 익히기 위한 실습 기록 |

위 기능 목록은 저장소의 코드 구성을 설명합니다. 파일별 수업 예제와 AI 재구현의 구분은 개별 작업 기록을 기준으로 확인합니다.

## 검증과 리소스

실행·빌드 결과는 각 실습 당시의 기록을 기준으로 하며 이 문서의 수정으로 새로운 검증 결과가 추가된 것은 아닙니다. 수업 에셋과 외부 샘플·라이브러리는 각 제공자의 라이선스를 따릅니다.
