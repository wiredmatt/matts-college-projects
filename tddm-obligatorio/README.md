# obligatorio-tddm

BabyTracker is an app that allows you to track events related to your baby (duh).

## Build it

```sh
npm i
npx cap add android # only needed if the `android` directory is not already present.
npx cap sync # copy files over to the `android` directory

cd android
JAVA_HOME=/home/youruser/android-studio/jbr ./gradlew assembleDebug # build the apk

# apk artifact can be found in ./android/app/build/outputs/apk/debug/app-debug.apk
```
