# 베이스 이미지 설정
FROM mcr.microsoft.com/openjdk/jdk:21-ubuntu

# 작업 디렉토리 설정
WORKDIR /app

# 빌드 파일 복사
COPY ./build/libs/sbweb-0.0.1-SNAPSHOT.jar /app/app.jar
COPY ./opentelemetry-javaagent.jar /app

ENV OTEL_EXPORTER_OTLP_PROTOCOL=grpc \
    OTEL_EXPORTER_OTLP_ENDPOINT=OTEL_EXPORTER_OTLP_ENDPOINT \
    OTEL_SERVICE_NAME=OTEL_SERVICE_NAME \
    OTEL_BLRP_SCHEDULE_DELAY=OTEL_BLRP_SCHEDULE_DELAY \
    OTEL_BSP_SCHEDULE_DELAY=OTEL_BSP_SCHEDULE_DELAY \
    OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES=OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EVENT_LOG_ATTRIBUTES \
    OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES=OTEL_DOTNET_EXPERIMENTAL_OTLP_EMIT_EXCEPTION_LOG_ATTRIBUTES

# 포트 노출
EXPOSE 8080

# 애플리케이션 실행
# ENTRYPOINT ["java","-javaagent:/app/opentelemetry-javaagent.jar", "-Xshare:off", "-jar","/app/app.jar"]칟
ENTRYPOINT ["java","-javaagent:/app/opentelemetry-javaagent.jar", "-jar","/app/app.jar"]