
<template>
  <v-row>
    <v-btn block @click="opoenOrClose()"> Open All </v-btn>
    <v-container
      v-if="$auth.loggedIn && $auth.user.role == 'admin'"
      class="d-flex flex-row ma-6"
    >
      <v-switch v-model="decending" label="decending"></v-switch>
      <v-switch
        v-model="sortSubcategories"
        label="sortSubcategories"
      ></v-switch>
      <v-btn color="primary" @click.stop="SortCategories">
        Sort Categories</v-btn
      >
      <v-btn
        color="primary"
        @click.stop="addCategory = true"
      >
        Add Category</v-btn
      >
      <div v-if="selectNewParent">
        <v-btn
          color="primary"
          @click.stop="(newParentId = null), MoveCategory()"
        >
          Move to the top</v-btn
        >
      </div>
    </v-container>
    <u v-if="selected != null">
      <AddSubCategory v-model="addSubCat" :Parent="selected" />
      <AddCategory v-model="addCategory"/>
      <RenameCategory v-model="renameCat" :Category="selected" />
    </u>
    <Alert v-model="dialog" :errmesage="errorMessage" />

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
          <template v-if="active" v-slot:append="{ item }">
            <u v-if="$auth.loggedIn && $auth.user.role == 'admin'">
              <v-list v-if="!selectNewParent">
                <u v-if="item.subCategories[0] != null">
                  <v-menu offset-y :close-on-content-click="false">
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn color="primary" dark v-bind="attrs" v-on="on">
                        Sort Categories
                      </v-btn>
                    </template>
                    <v-list>
                      <v-switch
                        v-model="decendingSub"
                        label="decending"
                      ></v-switch>
                      <v-switch
                        v-model="sortSubcategoriesSub"
                        label="sortSubcategories"
                      ></v-switch>
                      <v-btn
                        color="primary"
                        @click="(selected = item), SortSubCategories()"
                      >
                        Sort Categories</v-btn
                      >
                    </v-list>
                  </v-menu>
                </u>
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
                <v-btn color="red" @click="(selected = item), DeleteCategory()">
                  Delete</v-btn
                >
                <v-btn
                  color="primary"
                  @click.stop="
                    (selectNewParent = true), (categoryToMoveId = item.id)
                  "
                >
                  Move Category</v-btn
                >
              </v-list>
              <div v-if="selectNewParent">
                <v-btn
                  color="primary"
                  @click.stop="(newParentId = item.id), MoveCategory()"
                >
                  Chose as new Parent</v-btn
                >
              </div>
            </u>
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
      selected: {
        type: Object,
        default: () => {},
      },
      openall: false,
      addSubCat: false,
      renameCat: false,
      decending: false,
      sortSubcategories: false,
      sortSubcategoriesSub: false,
      decendingSub: false,
      selectNewParent: false,
      dialog: true,
      addCategory:false,
      categoryToMoveId: "",
      newParentId: "",
      errorMessage: "",
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
    async DeleteCategory() {
      await this.$axios
        .delete("/api/Categories/" + this.selected.id)
        .catch((error) => {
          (this.errorMessage = error.response.data.message),
            (this.dialog = false);
        });
      this.$nuxt.refresh();
    },
    async SortCategories() {
      await this.$axios
        .put("/api/CategoryNodes/SortNodes/", {
          decending: this.decending,
          sortSubcategories: this.sortSubcategories,
        })
        .catch((error) => {
          (this.errorMessage = error.response.data.message),
            (this.dialog = false);
        });
      this.$nuxt.refresh();
    },
    async SortSubCategories() {
      await this.$axios
        .put("/api/CategoryNodes/SortNode/" + this.selected.id, {
          decending: this.decendingSub,
          sortSubcategories: this.sortSubcategoriesSub,
        })
        .catch((error) => {
          (this.errorMessage = error.response.data.message),
            (this.dialog = false);
        });
      this.$nuxt.refresh();
    },
    async MoveCategory() {
      await this.$axios
        .put("/api/CategoryNodes/MoveNode/" + this.categoryToMoveId, {
          id: this.newParentId,
        })
        .catch((err) => {
          (this.errorMessage = err.response.data.message),
            (this.dialog = false);
        });
      this.categoryToMoveId = "";
      this.newParentId = "";
      this.$nuxt.refresh();
      this.selectNewParent = false;
    },
  },
};
</script> 
