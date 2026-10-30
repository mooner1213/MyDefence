# MyDefence · 타워 디펜스 학습 프로젝트

타워를 선택해 설치하고 적 웨이브에 대응하는 Unity 타워 디펜스 학습 프로젝트입니다.

## 확인할 기능

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


## 작업 범위와 검증

수업 기반 학습 프로젝트입니다. 위 목록은 저장소의 코드 구성에 근거한 기능 설명이며 수업 예제와 개인 확장 기능의 구분은 별도 작업 기록을 기준으로 합니다. 이번 문서 정리는 새로 수행한 실행·빌드 시험의 결과를 의미하지 않습니다.

외부 에셋과 샘플은 자체 제작 코드와 구분하며 각 리소스의 라이선스를 따릅니다.
