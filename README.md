# Chowol
- "Chowol"은 MMORPG 게임 "로스트 아크"의 초월 시스템을 유니티로 구현한 프로젝트입니다.
- 이 프로젝트는 강화 시스템의 작동 방식을 이해하기 위해 만들었습니다.

# 주요 기능
- 플레이어는 원하는 장비 부위와 초월 단계를 선택하여 초월할 수 있으며, 사용된 재화를 확인할 수 있습니다.

# 출처
<시스템>
- 이 프로젝트는 MMORPG 게임 "로스트 아크"의 "초월 시스템"을 참고하여 개발되었습니다.

<사운드>
- smilegate RPG
- https://gongu.copyright.or.kr/gongu/wrt/wrt/view.do?wrtSn=13252439&menuNo=200020

# Tasks
<구현>
- 특수 타일 효과 구현

<수정>
- availTiles에 추가 안되거나 안 지워지는 경우 찾아 수정
- special 타일 안부서지는 현상 수정
- 카드 별 부수는 타일 위치 수정
- 정화 카드 dist에 사용할 수 있도록 수정*
- 리팩터링

<에러>
- 특정 상황에서 카드 선택 후 블럭 클릭 시 GetSelectedCard에서 NullReferenceException에러 발생