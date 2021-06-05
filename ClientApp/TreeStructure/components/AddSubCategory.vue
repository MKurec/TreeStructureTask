<template>
  <v-row justify="center">
    <v-dialog v-model="addSubCatDialog" max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline">Parent Cateory Name {{Parent.name}}</span>
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
          <v-btn color="blue darken-1" text @click.stop="addSubCatDialog = false">
            Close
          </v-btn>
          <v-btn color="blue darken-1" text @click="addSubCategory ">
            Add SubCategory
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
     Parent: {
              type: Object,
              default: () => {},
          }
  },
  methods: {
    async addSubCategory() {
      await this.$axios
        .post("/api/Categories/" + this.Parent.id, {
          name: this.name,
        })
        .catch((error) => {
          console.log(error);
        });
        this.addSubCatDialog=false;
      this.$nuxt.refresh();
    },
  },
  computed: {
    addSubCatDialog: {
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