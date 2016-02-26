var gulp = require('gulp');
var shell = require('gulp-shell');
var concat = require('gulp-concat');
var watch = require('gulp-watch');
var path = require('path');

var assets = {
    jsdest: path.join(__dirname, 'wwwroot', 'js'),
    js: [
        path.join(__dirname, '**/scripts/**/*.js')
    ],
    cssdest: path.join(__dirname, 'wwwroot', 'css'),
    css: [
        path.join(__dirname, '**/styles/**/*.css')
    ]
};

gulp.task('build:js', function () {
    return gulp.src(assets.js)
        .pipe(concat('app.js'))
        .pipe(gulp.dest(assets.jsdest));
});

gulp.task('build:css', function(){
    return gulp.src(assets.css)
        .pipe(concat('styles.css'))
        .pipe(gulp.dest(assets.cssdest));
});

gulp.task('build:dnx', shell.task(['dnu publish']));

gulp.task('build:client', ['build:js', 'build:css']);

gulp.task('build:all', ['build:client', 'build:dnx'])