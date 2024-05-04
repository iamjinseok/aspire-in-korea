# aspire-in-korea
## 개요
.net aspire로 java, react, python 등의 개발언어 및 framework를 오케스트레이션 하는 샘플입니다.

## demo source 디렉토리 설명
| 디렉토리명 | 설명 | 비고 |
| --------- | ---- | --- |
| AspireJavaScript.React | react front-end sample | ms sample에서 수정, 직접 실행 |
| cmd | windows batch file 실행 샘플 | 직접 실행 |
| demo.ApiService | webapi backend asp.net core | aspire starter application |
| demo.AppHost | aspire app host application | aspire starter application |
| demo.ServiceDefaults | aspire service defaults 구현 프로제트 | aspire starter application |
| demo.Web | blazor interactive server front-end | aspire starter application |
| fastapi | python fast api 샘플 | docker image로 생성 |
| sbweb | java, springboot 샘플 | docker image로 생성 |

## AspireJavaScript.React
- 해당 디렉토리에서 아래 명령 실행(아래 명령으로 react에서 필요로 하는 라이브러리 코드를 npm으로 설치)
  ```
  npm install
  ```
- aspire app host에서 `services__apiservice__http__0` 환경 변수로 backend의 endpoint를 전달하는데 해당 디렉토리의 `.env`파일을 참고 

- microsoft의 [readme](https://github.com/dotnet/aspire-samples/tree/main/samples/AspireWithJavaScript/AspireJavaScript.React)를 참고

참고 자료
- [JavaScript OpenTelemetry Getting Started](https://opentelemetry.io/docs/languages/js/getting-started/)
- [.NET Aspire with Angular, React, and Vue](https://learn.microsoft.com/en-us/samples/dotnet/aspire-samples/aspire-angular-react-vue/)
- [Integrating Angular, React, and Vue with .NET Aspire](https://github.com/dotnet/aspire-samples/tree/main/samples/AspireWithJavaScript)

## cmd
- 도스창에서 사용하는 명령어를 실행시킬 수 있을 것 같다고 생각하여 만든 샘플
- 시간(분)을 인수로 받아서 단순히 기다리는 `test.bat`파일을 실행

## aspire starter application
- demo.ApiService, demo.AppHost, demo.ServiceDefaults, demo.Web
- 기본 생성되는 프로젝트 4개
- ApiService
  - react에서 이 프로젝트로 weather data를 fetch하여 가져오는데 CORS 이슈가 발생하므로 이에 대한 설정 추가
    ```
    builder.Services.AddCors();
    ```
    ```
    app.UseCors(static builder =>
      builder.AllowAnyMethod()
          .AllowAnyHeader()
          .AllowAnyOrigin());
    ```
  - 샘플이므로 모두 허용
- App Host
  - 오케스트레이션 하기 위한 코드 추가
  - http에서 실행하기 위해서 preview 5부터 `launchSettings.json`에 아래 설정 필요
    ```
    "ASPIRE_ALLOW_UNSECURED_TRANSPORT": "true"
    ```

## fastapi
- python으로 webapi framework인 fastapi를 docker image로 생성하는 샘플
- [fastapi의 예제](https://fastapi.tiangolo.com/ko/deployment/docker/) 활용
- 해당 폴더에서 아래 명령어로 docker image 생성
  ``` 
  docker build -t aspire-fastapi:0.0.1 .
  ```
- Dockerfile에서 ENV 설정이 중요
  ```
  ENV OTEL_EXPORTER_OTLP_PROTOCOL=grpc
  ENV OTEL_SERVICE_NAME=OTEL_SERVICE_NAME
  ENV OTEL_EXPORTER_OTLP_ENDPOINT=OTEL_EXPORTER_OTLP_ENDPOINT
  ENV OTEL_TRACES_EXPORTER="otlp"
  ENV OTEL_METRICS_EXPORTER="otlp"

  ENV OTEL_BLRP_SCHEDULE_DELAY=OTEL_BLRP_SCHEDULE_DELAY 
  ENV OTEL_BSP_SCHEDULE_DELAY=OTEL_BSP_SCHEDULE_DELAY
  ```
  - 현재는 프로토콜을 grpc만 지원함
  - aspire의 버전에 따라 설정값이 계속 바뀌는 것으로 보이니 GA시 확인할 필요가 있음
  - [opentelemetry의 python 매뉴얼](https://opentelemetry.io/docs/languages/python/automatic/)

## sbweb
- spring boot으로 만든 JAVA 샘플
- `RollController.java`파일에 controller가 한 개있음
- 처리하지 않았지만 react frontend에서 접속하려면 CORS 처리 필요
- [opentelemetry의 JAVA 매뉴얼](https://opentelemetry.io/docs/languages/java/automatic/)
- Dockerfile에서 ENV 설정이 중요하고 JVM에 javaagent를 사용하여 편리
  ```
  ENV OTEL_EXPORTER_OTLP_PROTOCOL=grpc \
      OTEL_EXPORTER_OTLP_ENDPOINT=OTEL_EXPORTER_OTLP_ENDPOINT \
      OTEL_SERVICE_NAME=OTEL_SERVICE_NAME \
      OTEL_BLRP_SCHEDULE_DELAY=OTEL_BLRP_SCHEDULE_DELAY \
      OTEL_BSP_SCHEDULE_DELAY=OTEL_BSP_SCHEDULE_DELAY \
      OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES=OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES \
      OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES=OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES
  ```
- `opentelemetry-javaagent.jar`파일을 다운 받아 java agent로 사용

# 참고자료
- [.NET Aspire telemetry](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/telemetry)
- [OpenTelemetry Getting Started](https://opentelemetry.io/docs/getting-started/)
- [OTLP Exporter Configuration](https://opentelemetry.io/docs/languages/sdk-configuration/otlp-exporter)