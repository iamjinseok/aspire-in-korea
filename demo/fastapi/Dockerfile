FROM python:3.9

WORKDIR /code

COPY ./requirements.txt /code/requirements.txt

RUN pip install --no-cache-dir --upgrade -r /code/requirements.txt
RUN pip install opentelemetry-distro opentelemetry-exporter-otlp
RUN opentelemetry-bootstrap -a install

# 
COPY ./main.py /code/

ENV OTEL_EXPORTER_OTLP_PROTOCOL=grpc
ENV OTEL_SERVICE_NAME=OTEL_SERVICE_NAME
ENV OTEL_EXPORTER_OTLP_ENDPOINT=OTEL_EXPORTER_OTLP_ENDPOINT
ENV OTEL_TRACES_EXPORTER="otlp"
ENV OTEL_METRICS_EXPORTER="otlp"

ENV OTEL_BLRP_SCHEDULE_DELAY=OTEL_BLRP_SCHEDULE_DELAY 
ENV OTEL_BSP_SCHEDULE_DELAY=OTEL_BSP_SCHEDULE_DELAY

# 
CMD [ "opentelemetry-instrument", "uvicorn", "main:app", "--host", "0.0.0.0", "--port", "80"]