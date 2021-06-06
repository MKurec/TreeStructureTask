<template>
  <v-app-bar dense color="#43a047" dark max-height="50" outlined>
    <template v-slot:img="{ props }">
      <v-img
        v-bind="props"
        gradient="to top right, rgba(55,236,186,.7), rgba(25,32,72,.7)"
      ></v-img>
    </template>




    <v-spacer></v-spacer>


    <div v-if="$auth.loggedIn">
      <v-btn @click="logout" icon>
      <v-icon>fas fa-sign-out-alt</v-icon>
      </v-btn>
    </div>
    <div v-else>
      <v-row>

        <v-col class="d-flex align-start pr-8"
          ><v-btn color="primary" @click.stop="loginDialog = true">
            Zaloguj </v-btn
          ><Login v-model="loginDialog" />
        </v-col>
      </v-row>
    </div>
  </v-app-bar>
</template>
<script>
export default {
  name: "VmHeader",
  methods: {
    async logout() {
      await this.$auth.logout();
    },
  },
  data() {
    return {
      loginDialog: false,
    };
  },
};
</script>