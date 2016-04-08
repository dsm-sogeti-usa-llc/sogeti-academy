/// <binding AfterBuild='build:client' />
var gulp = require('gulp');
var concat = require('gulp-concat');
var watch = require('gulp-watch');
var replace = require('gulp-replace');
var path = require('path');

var assets = {
    jsdest: path.join(__dirname, 'js'),
    js: [
        path.join(__dirname, '**/scripts/**/*.js')
    ],
    cssdest: path.join(__dirname, 'css'),
    css: [
        path.join(__dirname, '**/styles/**/*.css')
    ]
};

gulp.task('build:js', function () {
    return gulp.src(assets.js)
        .pipe(concat('app.js'))
        .pipe(replace('$apiUrl$', process.env['Topics:ApiUrl']))
        .pipe(gulp.dest(assets.jsdest));
});

gulp.task('build:css', function(){
    return gulp.src(assets.css)
        .pipe(concat('styles.css'))
        .pipe(gulp.dest(assets.cssdest));
});

gulp.task('build:client', ['build:js', 'build:css']);