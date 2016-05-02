/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    sqlcmd = require('sqlcmd-runner'),
    run = require('gulp-run');

var paths = {
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean:db", function () {
    console.log('test');
    return sqlcmd({
        server: 'localhost\\SQLEXPRESS',
        database: 'playerratings',
        port: 1433,
        username: 'sa',
        password: '1',
        trustServerCert: true,
        failOnSqlErrors: true,
        query: 'USE [master]; IF DB_ID (N\'playerratings\') IS NOT NULL BEGIN DROP DATABASE [playerratings] END; CREATE DATABASE [playerratings];'
        //errorRedirection: true
    });
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);

/*
gulp.task("db:resetToFirstRelease", ["clean:db"], function(cb) {
    new run.Command('dnx ef database update 20160303175621_FirstRelease').exec('', cb);
});

gulp.task("db:removeMigrations", ["db:resetToFirstRelease"], function (cb) {
    new run.Command('dnx ef migrations remove').exec('', cb);
});

gulp.task("db:addSecondReleaseMigration", ["db:removeMigrations"], function (cb) {
    new run.Command('dnx ef migrations add SecondRelease').exec('', cb);
});

gulp.task("db:update", ["db:addSecondReleaseMigration"], function (cb) {
    new run.Command('dnx ef database update').exec('', cb);
});*/