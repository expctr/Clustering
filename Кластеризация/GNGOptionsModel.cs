using ClusteringLib;

namespace Кластеризация {

    public class GNGOptionsModel {

        GNGOptionsForm form;

        public ClusteringOptions Options;

        public GNGOptionsModel(GNGOptionsForm form, MainModel mainModel) {
            this.form = form;
            Options = mainModel.GetOptions();
        }

    }

}