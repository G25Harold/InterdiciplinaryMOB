FROM oven/bun:1
LABEL authors="G25"

WORKDIR /app

# Note: Using a wildcard bun.lock* handles both bun.lock and bun.lockb formats!
COPY package.json bun.lock* ./
RUN bun install --frozen-lockfile

COPY . .

ENV NODE_ENV=production
ENV PORT=3000

# Run as a non-root user for security
USER bun

# Match the EXPOSE port to your ENV PORT
EXPOSE 3000

CMD ["bun", "run", "src/index.ts"]

