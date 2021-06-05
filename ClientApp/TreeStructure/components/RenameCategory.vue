<template>
  <v-row justify="center">
    <v-dialog v-model="renameCatDialog" max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline">Nowa Nazwa Kategori {{Category.name}}</span>
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
          <v-btn color="blue darken-1" text @click.stop="renameCatDialog = false">
            Close
          </v-btn>
          <v-btn color="blue darken-1" text @click="renameCategory ">
            Rename Category
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
export default {
  data() {
    return {
        name: '',
        show1: false,
    }
  },
  props: {
     value: Boolean,
     Category: {
              type: Object,
              default: () => {},
          }
  },
  methods: {
    async renameCategory() {
      await this.$axios
        .put("/api/Categories/" + this.Category.id, {
          name: this.name,
        })
        .catch((error) => {
          console.log(error);
        });
        this.renameCatDialog=false;
      this.$nuxt.refresh();
    },
  },
  computed: {
    renameCatDialog: {
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