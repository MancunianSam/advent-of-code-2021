import sbt._

object Dependencies {
  lazy val scalaCacheVersion = "1.0.0-M6"
  lazy val scalaCache = "com.github.cb372" %% "scalacache-core" % scalaCacheVersion
  lazy val scalaCacheRedis = "com.github.cb372" %% "scalacache-redis" % scalaCacheVersion



}
