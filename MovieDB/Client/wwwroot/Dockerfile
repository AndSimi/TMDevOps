FROM nginx
COPY . /usr/share/nginx/html
COPY expose-env.sh /docker-entrypoint.d/40-expose-env.sh
RUN chmod 777 /docker-entrypoint.d/40-expose-env.sh