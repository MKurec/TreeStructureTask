<template >
  <div>
    <v-dialog v-model="alert" persistent max-width="600px">
      <v-alert v-model="alert" type="warning" dismissible elevation="2">
        {{ errmesage }}
      </v-alert>
    </v-dialog>
  </div>
</template>
<script>
export default {
  props: {
    value: Boolean,
    errmesage: "",
  },
  computed: {
    alert: {
      get() {
        return !this.value;
      },
      set(value) {
        this.$emit("input", !value);
      },
    },
  },
};
</script>