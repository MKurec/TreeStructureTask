<template>
  <v-row justify="center">
    <v-dialog v-model="addCatDialog" max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline">Enter new name </span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field label="Name*" v-model="name" required></v-text-field>
              </v-col>
             </v-row>
          </v-container>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click.stop="addCatDialog = false">
            Close
          </v-btn>
          <v-btn color="blue darken-1" text @click="addCategory ">
            Add Category
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
        <Alert v-model="dialog" :errmesage="errorMessage" />
  </v-row>
</template>

<script>
export default {
  data() {
    return {
        show1: false,
        errorMessage: "",
        dialog:true,
        name: ""
    }
  },
  props: {
     value: Boolean,
  },
  methods: {
    async addCategory() {
      await this.$axios
        .post("/api/Categories/", {
          name: this.name,
        })
        .catch((error) => {
          (this.errorMessage = error.response.data.message),
            (this.dialog = false);
        });
        this.addCatDialog=false;
      this.$nuxt.refresh();
    },
  },
  computed: {
    addCatDialog: {
      get () {
        return this.value
      },
      set (value) {
         this.$emit('input', value)
      },
    },
  }
}
</script>