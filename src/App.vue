<template>

  <main class="background bg-steelteal flex w-screen h-screen justify-center items-center"> 
    
    <QuizSelector v-if="store.quizModal" v-show="store.isLoggedIn == true" />
    <QuestionList v-if="store.questionModal" />
    <NewQuiz v-if="store.newQuizModal" />
    <NewQuestion v-if="store.newQuestionModal" />

    <!-- Top level container to darken the background and make contents behind unselectable -->
    <div v-if="store.isLoggedIn == false" class="absolute flex justify-center items-center bg-black bg-opacity-60 w-screen h-screen z-1000"
    >
      <div class="bg-etonblue rounded-lg px-14 w-1/2 py-8 flex-none shadow-lg container absolute z-40">
        <p class="font-bold text-4xl flex justify-center items-center p-5"> Please Log In </p>

        <!-- Container for login input fields -->
        <div class="font-bold text-2xl flex flex-col justify-center items-left">
          <p class="pb-2" > Username: </p>
          <input type="username" v-model="store.username" class="rounded-lg p-2">
        </div>

        <div class="font-bold text-2xl flex flex-col justify-center items-left">
          <p class="pb-2" > Password: </p>
          <input type="password" v-model="store.password" class="rounded-lg p-2">
        </div>

            <div class="flex justify-center items-center">
                <button @click="setAccess()" class="bg-white hover:bg-black hover:text-white py-2 mt-4 w-40 cursor-pointer rounded-full text-center text-xl font-bold">
                    Sign In
                </button>
            </div>
      </div>
    </div>
  
  </main>
</template>

<script lang="ts">
import { defineComponent, ref, Ref } from 'vue';
import { useStore } from '@/store/Store';

import * as userInfo from "@/users/users";

import QuizSelector from '@/components/QuizSelector.vue';
import QuestionList from '@/components/QuestionList.vue';
import NewQuiz from '@/components/NewQuiz.vue';
import NewQuestion from '@/components/NewQuestion.vue';

export default defineComponent({
  name: 'App',
  components: {
    QuizSelector,
    QuestionList,
    NewQuiz
  },

  setup()
  {
    const store = useStore();
    store.getQuizzes();

    function setAccess():void
    {
        if (store.username== userInfo.editUsername && store.password== userInfo.editPassword)
        {
          store.userRole = userInfo.editPermission;
          store.login();

          console.log(localStorage)
        }
        else if (store.username == userInfo.viewUsername && store.password== userInfo.viewPassword)
        {
          store.userRole = userInfo.viewPermission;
          store.login();
        }
        else if (store.username == userInfo.restrictedUsername && store.password == userInfo.restrictedPassword)
        {
          store.userRole = userInfo.restrictedPermission;
          store.login();
        }
        else 
        {
          window.alert('Incorrect Username or Password');
        }

    }

    return {
      store,
      QuizSelector,
      QuestionList,
      NewQuiz,
      NewQuestion,
      setAccess
    }
  }
});
</script>
