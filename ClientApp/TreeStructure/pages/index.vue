
<template>
  <v-row>
    <v-btn block @click="opoenOrClose()"> Open All </v-btn>
    <u v-if="selected != null">
      <AddSubCategory v-model="addSubCat" :Parent="selected" />
      <RenameCategory v-model="renameCat" :Category="selected" />
    </u>
    <ul class="flex flex-col lg:flex-row gap-10">
      <v-col style="min-width: 600px" flex-shrink="1">
        <v-treeview
          style="min-width: 199px"
          :open-all="openall"
          :active.sync="active"
          activatable
          :items="items"
          item-children="subCategories"
          return-object
          ref="myCategoryView"
        >
          <v-divider inset></v-divider>
          <template v-if="active" v-slot:append="{ item }">
            <v-btn
              color="primary"
              @click.stop="(renameCat = true), (selected = item)"
            >
              Rename Category</v-btn
            >
            <v-btn
              color="primary"
              @click.stop="(addSubCat = true), (selected = item)"
            >
              Add Sub Category</v-btn
            >
            <v-btn
              color="red"
              @click="(deleteCategory), (selected = item)"
            >
              Delete </v-btn
            >
          </template>
        </v-treeview>
      </v-col>
    </ul>
  </v-row>
</template>

<script>
import AddSubCategory from "~/components/AddSubCategory.vue";
export default {
  components: { AddSubCategory },
  data() {
    return {
      items: [],
      active: [],
      selected: [],
      openall: false,
      addSubCat: false,
      renameCat: false,
    };
  },
  async fetch() {
    let items = await this.$axios.$get("/api/Categories/Tree");
    this.items = items;
  },
  methods: {
    opoenOrClose: function () {
      this.openall = !this.openall;
      this.$refs.myCategoryView.updateAll(this.openall);
    },
    async deleteCategory() {
      await this.$axios
        .delete("/api/Categories/" + this.selected.id)
        .catch((error) => {
          console.log(error);
        });
      this.$nuxt.refresh();
    },
  },
};
</script> 
