using ClusteringLib;

namespace Кластеризация {

    public class FORELOptionsModel {

        FORELOptionsForm form;

        public ClusteringOptions Options;

        public FORELOptionsModel(FORELOptionsForm form, MainModel mainModel) {
            this.form = form;
            Options = mainModel.GetOptions();
        }

    }

}